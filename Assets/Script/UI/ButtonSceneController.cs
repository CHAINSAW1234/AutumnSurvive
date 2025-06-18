using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneController : MonoBehaviour
{
    public void ToStartScene() => ChangeScene(Defines.Scene.StartScene);
    public void ToGamePlayScene() => ChangeScene(Defines.Scene.GamePlayScene);
    public void ResetCurretScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Managers.Instance.Clear();
    } 
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ChangeScene(Defines.Scene scene)
    {
        SceneManager.LoadScene(scene.ToDescription());
        Managers.Instance.Clear();
    }
}
