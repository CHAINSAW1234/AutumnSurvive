using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_Instance;
    private InputManager inputManager = new InputManager();
    private ResourceManager resourceManager = new ResourceManager();
    private PoolManager poolManager = new PoolManager();
    private static Managers Instance { get { Init(); return s_Instance; } }
    public static InputManager Input { get { return  Instance.inputManager; } }
    public static ResourceManager Resource {  get { return Instance.resourceManager; } }
    public static PoolManager Pool {  get { return Instance.poolManager; } }
    
    private static void Init()
    {
        if(s_Instance == null)
        {
            GameObject gameobject = GameObject.Find("@Managers");
            if (null == gameobject)
            {
                gameobject = new GameObject { name = "@Managers" };
                gameobject.AddComponent<Managers>();
            }

            s_Instance = gameobject.GetComponent<Managers>();
            s_Instance.InitManagers();
        }
    }
    private void InitManagers()
    {
        Pool.Init(gameObject.transform);
    }

    void Update()
    {
        Input.Update();
    }

    void Clear()
    {
        Input.Clear();
        Pool.Clear();
    }
}
