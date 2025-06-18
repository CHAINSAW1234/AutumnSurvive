using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;

public class SkillSlotController : MonoBehaviour
{
	public enum SkillUIText
	{
		Name,
		Level,
		Description,
		LevelInfo
	}


	private Defines.Skill skill;
	public Defines.Skill Skill { get => skill; set { skill = value; InitUI(); UpdateUIs(); } }

    // 바꿔야 되는거 이미지, 설명, 스킬 이름, 레벨 ,설명, 레벨 GUI화
    [SerializeField]
	Image skillIcon;
    [SerializeField]
    TextMeshProUGUI[] texts = new TextMeshProUGUI[System.Enum.GetValues(typeof(SkillUIText)).Length];
    [SerializeField]
    Transform levelManagementUI;

	[SerializeField]
	Sprite levelOnImage;
    [SerializeField]
    Sprite levelOffImage;

    private void OnEnable()
    {

    }

    private void InitUI()
    {
        int level = Math.Max(PlayerDataController.Instance.GetSkillLevelAt(skill), 1);

        skillIcon.sprite = Managers.Resource.Load<Sprite>($"Sprites/" + skill.ToDescription());
        texts[(int)SkillUIText.Name].text = skill.ToDescription();
        texts[(int)SkillUIText.Level].text = "Lv." + level;
        texts[(int)SkillUIText.Description].text = "test\ntest\ntest\ntest";//Managers.Data.SkillDict[skill].Description;
        texts[(int)SkillUIText.LevelInfo].text = Managers.Data.SkillDict[skill].GetLevelInfo();

        for (int i = 1; i < transform.childCount - 1; ++i)
        {
            Managers.Resource.Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Managers.Data.SkillDict[skill].MaxLevel; ++i)
        {
            GameObject obj = new GameObject("SkillLevelCounter", typeof(RectTransform), typeof(Image));
            obj.transform.SetParent(levelManagementUI, false);

            RectTransform transform = obj.GetComponent<RectTransform>();
            transform.sizeDelta = new Vector2(30, transform.sizeDelta.y);
            transform.anchoredPosition = Vector2.zero;

            Image img = obj.GetComponent<Image>();
            img.sprite = levelOffImage;

            obj.transform.SetSiblingIndex(i + 1);

        }
    }

    private void UpdateUIs()
	{
        int level = Math.Max(PlayerDataController.Instance.GetSkillLevelAt(skill), 1);

        texts[(int)SkillUIText.Level].text = "Lv." + level;
		//texts[(int)SkillUIText.LevelInfo].text = Managers.Data.SkillDict[skill].GetLevelInfo();

		for(int i=0;i<Managers.Data.SkillDict[skill].MaxLevel; ++i)
		{
            GameObject obj = levelManagementUI.transform.GetChild(i + 1).gameObject;

            Image img = obj.GetComponent<Image>();
            img.sprite = i < level ? levelOnImage : levelOffImage;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

    }

    public void LevelUp()
    {
        if(PlayerDataController.Instance.LeftSkillPoint > 0)
        {
            PlayerDataController.Instance.SetSkillLevelAt(skill, PlayerDataController.Instance.GetSkillLevelAt(skill) + 1);
            PlayerDataController.Instance.LeftSkillPoint -= 1;
            UpdateUIs();
        }
    }

    public void LevelDown()
    {
        int level = PlayerDataController.Instance.GetSkillLevelAt(skill);

        if(level > 1)
        {
            PlayerDataController.Instance.SetSkillLevelAt(skill, PlayerDataController.Instance.GetSkillLevelAt(skill) - 1);
            PlayerDataController.Instance.LeftSkillPoint += 1;
            UpdateUIs();
        }


    }

}
