using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private Slider slider;

    private PauseController pauseController;

    private const float speedBias = 100f;

    private void Awake()
    {
        pauseController = gameObject.GetOrAddComponent<PauseController>();
    }

    private void OnEnable()
    {
        pauseController.Pause();
        GamePlayController gamePlayController = FindAnyObjectByType<GamePlayController>();
        timeText.text = gamePlayController.GetGamePlayTime();
        scoreText.text = ((int)gamePlayController.Score).ToString();
        levelText.text = "LV " + PlayerDataController.Instance.Level.ToString();
        slider.value = (float)PlayerDataController.Instance.CurrentExp / (float)Managers.Data.PlayerLevelDict[PlayerDataController.Instance.Level].exp;

        StartCoroutine(AddExp(gamePlayController.GetExp()));
        //StartCoroutine(AddExp(100));
    }

    IEnumerator AddExp(float exp)
    {
        while (exp > 0 && PlayerDataController.Instance.Level < Managers.Data.MaxLevel)
        {
            float currentExp = PlayerDataController.Instance.CurrentExp;
            float targetExp = Managers.Data.PlayerLevelDict[PlayerDataController.Instance.Level].exp;
            while (currentExp < targetExp)
            {
                currentExp += Time.unscaledDeltaTime * speedBias;
                exp -= Time.unscaledDeltaTime * speedBias;

                slider.value = currentExp / targetExp;

                if (currentExp >= targetExp)
                {
                    exp += currentExp - targetExp;
                    break;
                }

                if(exp <= 0)
                {
                    currentExp += exp;
                    PlayerDataController.Instance.CurrentExp = (int)currentExp;
                    yield break;
                }

                yield return null;
            }

            PlayerDataController.Instance.LevelUp();
            levelText.text = "LV " + PlayerDataController.Instance.Level.ToString();
            slider.value = 0;

            if (Managers.Data.PlayerLevelDict[PlayerDataController.Instance.Level].skillPoint == true)
            {
                Managers.Resource.Instantiate("UI/GamePlayScene/Skill Point Panel", transform.parent);
            }
            else
            {
                SkillUnlockPanelController controller = Managers.Resource.Instantiate("UI/GamePlayScene/Skill Unlock Panel", transform.parent).GetComponent<SkillUnlockPanelController>();
                controller.Skill = Managers.Data.PlayerLevelDict[PlayerDataController.Instance.Level].skillUnlock;
            }
        }

        yield break;
    }
}
