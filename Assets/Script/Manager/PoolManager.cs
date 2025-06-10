using System.Collections.Generic;
using UnityEngine;


public class PoolManager
{
    private class Pool
    {
        public GameObject Original { get; private set; } = null;
        public Transform Root = null;
        private readonly Queue<Poolable> poolQueue = new Queue<Poolable>();

        public void Init(GameObject origin, int count = 20)
        {
            Original = origin;
            Root = new GameObject($"{Original.name}_root").transform;
            for (int i = 0; i < count; ++i)
            {
                Enqueue(Create());
            }
        }

        public Poolable Create()
        {
            GameObject obj = Object.Instantiate(Original);
            obj.name = Original.name;

            return obj.GetOrAddComponent<Poolable>();
        }

        public void Enqueue(Poolable poolable)
        {
            if (poolable == null)
            {
                return;
            }

            poolable.transform.parent = Root.transform;
            poolable.gameObject.SetActive(false);

            poolQueue.Enqueue(poolable);
        }
        public Poolable Dequeue(Transform transform = null)
        {
            Poolable poolable;
            if (poolQueue.Count > 0)
            {
                poolable = poolQueue.Dequeue();
            }
            else
            {
                poolable = Create();
                poolable.name = Original.name;
            }

            if(transform != null)
            {
                poolable.transform.parent = transform;
            }
            poolable.gameObject.SetActive(true);
            return poolable;
        }

        public void Clear()
        {
            foreach(var obj in poolQueue)
            {
                Object.Destroy(obj);
            }
            poolQueue.Clear();

            Object.Destroy(Root);
            Root = null;
            Object.Destroy(Original);
            Original = null;
        }
    }

    Transform root;
    private readonly Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

    public void Init(Transform parent)
    {
        if(root == null)
        {
            root = new GameObject("@PoolManager").transform;
            root.parent = parent;

            GameObject[] pools = Managers.Resource.LoadAll<GameObject>("Prefabs/");
            foreach (GameObject obj in pools)
            {
                Poolable pool = obj.GetComponent<Poolable>();
                if (pool != null)
                {
                    CreatePool(obj, pool.poolCreateCount);
                }
            }
        }
    }

    private void CreatePool(GameObject origin, int count = 10)
    {
        if (origin != null && !poolDict.ContainsKey(origin.name))
        {
            Pool pool = new Pool();
            pool.Init(origin, count);
            pool.Root.parent = root;
            poolDict[origin.name] = pool;
        }
    }

    public void Enqueue(Poolable obj)
    {
        if (obj != null && !poolDict.ContainsKey(obj.name))
        {
            Object.Destroy(obj.gameObject);
            return;
        }

        poolDict[obj.name].Enqueue(obj);
    }

    public Poolable Dequeue(string name, Transform transform = null)
    {
        if (!poolDict.ContainsKey(name))
        {
            GameObject prefab = Managers.Resource.Load<GameObject>(name);
            if (prefab != null)
            {
                Debug.LogWarning("MapObjectPool : Prefab Load Error");
                return null;
            }

            CreatePool(prefab);
        }

        return poolDict[name].Dequeue(transform);
    }

    public Poolable Dequeue(GameObject original, Transform transform = null)
    {
        if (original == null)
        {
            return null;
        }

        if (!poolDict.ContainsKey(original.name))
        {
            CreatePool(original);
        }

        return poolDict[original.name].Dequeue(transform);
    }

    public GameObject GetOriginal(string name)
    {
        if(!poolDict.ContainsKey(name))
        {
            return null;
        }

        return poolDict[name].Original;
    }
    public void Clear()
    {
        foreach(var pool in poolDict) {
            pool.Value.Clear();
        }
        poolDict.Clear();
    }
}

