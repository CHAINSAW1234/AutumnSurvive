using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

        public Poolable Create(Vector3 position = default)
        {
            GameObject obj = Object.Instantiate(Original, position, Quaternion.identity);
            obj.name = Original.name;

            return obj.GetOrAddComponent<Poolable>();
        }

        public void Enqueue(Poolable poolable)
        {
            if (poolable == null)
            {
                return;
            }

            poolable.transform.SetParent(Root);
            poolable.gameObject.SetActive(false);

            if(!poolQueue.Contains(poolable))
            {
                poolQueue.Enqueue(poolable);
            }
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
            }
            
            poolable.transform.parent = transform;
            if (transform == null)
            {
                SceneManager.MoveGameObjectToScene(poolable.gameObject, SceneManager.GetActiveScene());
            }
            
            poolable.gameObject.SetActive(true);

            return poolable;
        }

        public Poolable Dequeue(Vector3 position)
        {
            Poolable poolable;
            if (poolQueue.Count > 0)
            {
                poolable = poolQueue.Dequeue();
                poolable.transform.parent = null;
                SceneManager.MoveGameObjectToScene(poolable.gameObject, SceneManager.GetActiveScene());
                poolable.transform.position = position;
            }
            else
            {
                poolable = Create(position);
            }

            poolable.gameObject.SetActive(true);

            return poolable;
        }

        public void Clear()
        {
            foreach(var obj in poolQueue)
            {
                Object.Destroy(obj.gameObject);
            }
            poolQueue.Clear();

            Object.Destroy(Root.gameObject);
            Original = null;
        }
    }

    private Transform root;
    private readonly Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

    public void Init(Transform parent)
    {
        if(root == null)
        {
            root = new GameObject("@PoolManager").transform;
            root.parent = parent;
        }
    }

    private void CreatePool(GameObject origin)
    {
        if (origin != null && !poolDict.ContainsKey(origin.name))
        {
            int count = origin.GetOrAddComponent<Poolable>().poolCreateCount;

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

    public Poolable Dequeue(GameObject original, Vector3 position)
    {
        if (original == null)
        {
            return null;
        }

        if (!poolDict.ContainsKey(original.name))
        {
            CreatePool(original);
        }

        return poolDict[original.name].Dequeue(position);
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

