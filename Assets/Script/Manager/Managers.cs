using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_Instance;
    private InputManager inputManager = new InputManager();

    private static Managers Instance { get { Init(); return s_Instance; } }
    public static InputManager Input { get { return  Instance.inputManager; } }

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
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Input.Update();
    }
}
