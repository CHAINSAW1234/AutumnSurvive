using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;
    private InputManager inputManager = new InputManager();
    private ResourceManager resourceManager = new ResourceManager();
    private SoundManager soundManager = new SoundManager();
    private DataManager dataManager = new DataManager();
    private PoolManager poolManager = new PoolManager();

    public static Managers Instance { get { Init(); return instance; } }
    public static InputManager Input { get { return  Instance.inputManager; } }
    public static ResourceManager Resource {  get { return Instance.resourceManager; } }
    public static SoundManager Sound { get { return Instance.soundManager; } }
    public static DataManager Data { get { return Instance.dataManager; } }
    public static PoolManager Pool {  get { return Instance.poolManager; } }

    private static void Init()
    {
        if(instance == null)
        {
            GameObject gameObject = GameObject.Find("@Managers");
            if (null == gameObject)
            {
                gameObject = new GameObject { name = "@Managers" };
                gameObject.AddComponent<Managers>();
            }
            DontDestroyOnLoad(gameObject);

            instance = gameObject.GetComponent<Managers>();
            instance.InitManagers();
        }
    }

    private void InitManagers()
    {
        Sound.Init(gameObject.transform);
        Data.Init();
        Pool.Init(gameObject.transform);
    }

    private void Update()
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
