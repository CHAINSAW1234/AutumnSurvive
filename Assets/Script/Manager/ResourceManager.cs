using System.Threading;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if(index >=0)
            {
                name = name.Substring(index + 1);
            }

            GameObject gameObject = Managers.Pool.GetOriginal(name);
            if (gameObject != null)
            {
                return gameObject as T;
            }
        }

        return Resources.Load<T>(path);
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            return null;
        }

        if(prefab.GetComponent<Poolable>())
        {
            return Managers.Pool.Dequeue(prefab, parent).gameObject;
        }    

        GameObject obj = Object.Instantiate(prefab, parent);
        obj.name = prefab.name;
        return obj;
    }

    public GameObject Instantiate(string path, Vector3 position)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            return null;
        }

        if (prefab.GetComponent<Poolable>())
        {
            return Managers.Pool.Dequeue(prefab, position).gameObject;
        }

        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.name = prefab.name;
        return obj;
    }
    public void Destroy(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        Poolable pool = obj.GetComponent<Poolable>();
        if (pool != null)
        {
            Managers.Pool.Enqueue(pool);
            return;
        }

        Object.Destroy(obj);
    }
}

