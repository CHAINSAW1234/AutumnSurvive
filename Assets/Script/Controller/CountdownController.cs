using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    [SerializeField]
    private GamePlayController gamePlayController;

    private TextMeshProUGUI text;

    private const int count = 3;

    void Start()
    {
        if(gamePlayController == null)
        {
            gamePlayController = FindObjectOfType<GamePlayController>();
        }
        gamePlayController.Reset();
        text = GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0;

        text.text = count.ToString();
        StartCoroutine(Countdown());
    }

    void Update()
    {
        gamePlayController.Reset();
        text.alpha -= Time.unscaledDeltaTime;
    }

    IEnumerator Countdown()
    {
        for (int i = count; i > 0; --i)
        {
            yield return new WaitForSecondsRealtime(1);
            text.text = (i-1).ToString();
            text.alpha = 1;
        }

        text.text = "Start!";
        text.alpha = 1;
        yield return new WaitForSecondsRealtime(1);

        Managers.Sound.Play("BGM", Defines.Sound.Bgm);
        Time.timeScale = 1;
        gameObject.SetActive(false);    
    }
}
