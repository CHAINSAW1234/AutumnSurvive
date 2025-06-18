using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private TextMeshProUGUI skillPointText;


    private void Start()
    {
        if (skillPointText == null)
        {
            Debug.Log("skillPointText : null");
        }

        int childCount = content.transform.childCount;
        int length = Enum.GetValues(typeof(Defines.Skill)).Length;

        if (childCount < length)
        {
            for (int i = 0; i < length - childCount; ++i)
            {
                Managers.Resource.Instantiate("UI/StartScene/SkillSlot", content.transform);
            }
        }

        for (int i = 0; i < length; ++i)
        {

            GameObject obj = content.transform.GetChild(i).gameObject;

            obj.GetOrAddComponent<SkillSlotController>().Skill = (Defines.Skill)i;

            //if (PlayerDataController.Instance.SkillLevels[i] == 0)
            //{
            //    content.transform.GetChild(i).gameObject.SetActive(false);
            //}
        }
    }

    private void OnEnable()
    {
        UpdateSkillPointText();
        //UpdateScrollView();
    }

    public void UpdateSkillPointText()
    {
        skillPointText.text = PlayerDataController.Instance.LeftSkillPoint + "/" + PlayerDataController.Instance.TotalSkillPoint;
    }

    public void UpdateScrollView()
    {
        for (int i=0;i< Enum.GetValues(typeof(Defines.Skill)).Length; ++i)
        {
            if(PlayerDataController.Instance.SkillLevels[i] != 0)
            {
                content.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
