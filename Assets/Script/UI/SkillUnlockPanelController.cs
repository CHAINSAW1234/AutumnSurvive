using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillSlotController;

public class SkillUnlockPanelController : MonoBehaviour
{
    public Defines.Skill Skill {
        set
        {
            UpdateSkill(value);
        }
    }

    [SerializeField]
    Image skillIcon;
    [SerializeField]
    TextMeshProUGUI skillNameText;
    [SerializeField]
    TextMeshProUGUI skillDescriptionText;

    private void UpdateSkill(Defines.Skill skill)
    {
        skillIcon.sprite = Managers.Resource.Load<Sprite>($"Sprites/" + skill.ToDescription());
        skillNameText.text = skill.ToDescription();
        skillDescriptionText.text = "test\ntest\ntest\ntest";//Managers.Data.SkillDict[skill].Description;
    }
}
