using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static SkillSlotController;

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

    private const float SpeedBias = 1000f;

    private void OnEnable()
    {
        GamePlayController gamePlayController = FindAnyObjectByType<GamePlayController>();
        timeText.text = gamePlayController.GetGamePlayTime();
        scoreText.text = ((int)gamePlayController.Score).ToString();
        levelText.text = "LV " + PlayerDataController.Instance.Level.ToString();
        StartCoroutine(AddExp(gamePlayController.GetExp()));
    }

    IEnumerator AddExp(float exp)
    {
        exp += PlayerDataController.Instance.CurrentExp;
        PlayerDataController.Instance.CurrentExp = 0;

        while (exp > 0 && PlayerDataController.Instance.Level < Managers.Data.MaxLevel)
        {
            float currentExp = 0;
            float targetExp = Managers.Data.PlayerLevelDict[PlayerDataController.Instance.Level].exp;
            while (currentExp < targetExp)
            {
                currentExp += Time.deltaTime * SpeedBias;
                exp -= Time.deltaTime * SpeedBias;

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

            PlayerDataController.Instance.Level += 1;
            levelText.text = "LV " + PlayerDataController.Instance.Level.ToString();

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
