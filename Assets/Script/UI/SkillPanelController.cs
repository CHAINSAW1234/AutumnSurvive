using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject scrollView;

    [SerializeField]
    private TextMeshProUGUI skillPointText;


    private void Awake()
    {
        if(skillPointText == null)
        {
            Debug.Log("skillPointText : null");
        }
    }

    private void OnEnable()
    {
        UpdateSkillPointText();
    }

    public void UpdateSkillPointText()
    {
        skillPointText.text = PlayerDataController.Instance.LeftSkillPoint + "/" + PlayerDataController.Instance.TotalSkillPoint;
    }

    public void UpdateScrollView()
    {
        foreach(Transform child in scrollView.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }


        for(int i=0;i< Enum.GetValues(typeof(Defines.Skill)).Length; ++i)
        {
            if(PlayerDataController.Instance.SkillLevels[i] != 0)
            {
                //GameObject obj = new GameObject();
                //obj.transform.parent = scrollView.transform;
                //obj.update(skill);
            }
        }
    }
}
