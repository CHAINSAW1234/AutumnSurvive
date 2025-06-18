using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    private static Managers s_Instance;
    private InputManager inputManager = new InputManager();
    private ResourceManager resourceManager = new ResourceManager();
    private SoundManager soundManager = new SoundManager();
    private DataManager dataManager = new DataManager();
    private PoolManager poolManager = new PoolManager();

    public static Managers Instance { get { Init(); return s_Instance; } }
    public static InputManager Input { get { return  Instance.inputManager; } }
    public static ResourceManager Resource {  get { return Instance.resourceManager; } }
    public static SoundManager Sound { get { return Instance.soundManager; } }
    public static DataManager Data { get { return Instance.dataManager; } }
    public static PoolManager Pool {  get { return Instance.poolManager; } }

    private static void Init()
    {
        if(s_Instance == null)
        {
            GameObject gameObject = GameObject.Find("@Managers");
            if (null == gameObject)
            {
                gameObject = new GameObject { name = "@Managers" };
                gameObject.AddComponent<Managers>();
            }
            DontDestroyOnLoad(gameObject);

            s_Instance = gameObject.GetComponent<Managers>();
            s_Instance.InitManagers();
        }
    }
    private void InitManagers()
    {
        Sound.Init(gameObject.transform);
        Data.Init();
        Pool.Init(gameObject.transform);
    }

    void Update()
    {
        Input.Update();
    }

    public void Clear()
    {
        Input.Clear();
        Sound.Clear();
        //Data.Clear();
        Pool.Clear();
    }
}
