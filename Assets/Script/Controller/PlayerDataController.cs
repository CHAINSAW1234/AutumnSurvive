using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    private static PlayerDataController s_Instance;
    public static PlayerDataController Instance { get { Init(); return s_Instance; } }
    private static void Init()
    {
        if (s_Instance == null)
        {
            GameObject gameObject = GameObject.Find("@PlayerData");
            if (null == gameObject)
            {
                gameObject = new GameObject { name = "@PlayerData" };
                gameObject.AddComponent<PlayerDataController>();

            }
            DontDestroyOnLoad(gameObject);
            s_Instance = gameObject.GetComponent<PlayerDataController>();
        }
    }

    private int level;
    private int currentExp;
    private int totalSkillPoint;
    private int leftSkillPoint;
    private readonly List<int> skillLevels = new List<int>();

    public int Level { get => level; set { level = value; SavePlayerData(PlayerDatasKeys.level, level); } }
    public int CurrentExp { get => currentExp; set { CurrentExp = value; SavePlayerData(PlayerDatasKeys.currentExp, CurrentExp); } }
    public int TotalSkillPoint { get => totalSkillPoint; set { TotalSkillPoint = value; SavePlayerData(PlayerDatasKeys.totalSkillPoint, TotalSkillPoint); } }
    public int LeftSkillPoint { get => leftSkillPoint; set { LeftSkillPoint = value; SavePlayerData(PlayerDatasKeys.leftSkillPoint, LeftSkillPoint); } }
    public ReadOnlyCollection<int> SkillLevels { get => skillLevels.AsReadOnly(); }

    private void Start()
    {
        LoadAllPlayerData();
    }

    private void OnDestroy()
    {
        SaveAllPlayerData();
    }

    public int GetSkillLevelAt(Defines.Skill skill)
    {
        return skillLevels[(int)skill];
    }
    public void SetSkillLevelAt(Defines.Skill skill, int level)
    {
        int index = (int)skill;

        if(1 <= level && level <= Managers.Data.SkillDict[skill].MaxLevel) // 수정 요망
        {
            skillLevels[index] = level;
            SavePlayerData(PlayerDatasKeys.skillLevels, skillLevels);
        }
    }

    public void SaveAllPlayerData() {
        PlayerPrefs.SetInt(PlayerDatasKeys.level, level);
        PlayerPrefs.SetInt(PlayerDatasKeys.currentExp, currentExp);
        PlayerPrefs.SetInt(PlayerDatasKeys.totalSkillPoint, totalSkillPoint);
        PlayerPrefs.SetInt(PlayerDatasKeys.leftSkillPoint, leftSkillPoint);

        // List<int> → 문자열로 저장 (쉼표로 구분)
        string skillLevelsString = string.Join(",", skillLevels);
        PlayerPrefs.SetString(PlayerDatasKeys.skillLevels, skillLevelsString);

        PlayerPrefs.Save(); // 저장 적용
    }
    public void LoadAllPlayerData()
    {
        level = PlayerPrefs.GetInt(PlayerDatasKeys.level, PlayerDatasDefaults.level);
        currentExp = PlayerPrefs.GetInt(PlayerDatasKeys.currentExp, PlayerDatasDefaults.currentExp);
        totalSkillPoint = PlayerPrefs.GetInt(PlayerDatasKeys.totalSkillPoint, PlayerDatasDefaults.totalSkillPoint);
        leftSkillPoint = PlayerPrefs.GetInt(PlayerDatasKeys.leftSkillPoint, PlayerDatasDefaults.leftSkillPoint);

        // 문자열 → List<int> 변환
        string skillLevelsString = PlayerPrefs.GetString(PlayerDatasKeys.skillLevels, "");

        if (!string.IsNullOrEmpty(skillLevelsString))
        {
            string[] values = skillLevelsString.Split(',');
            for(int i = 0;i<values.Length; ++i)
            {
                if (int.TryParse(values[i], out int skillLevel))
                {
                    skillLevels[i] = skillLevel;
                }
            }
        }
    }
    private void SavePlayerData<T>(string key, T value)
    {
        switch (value)
        {
            case int intValue:
                PlayerPrefs.SetInt(key, intValue);
                break;

            case float floatValue:
                PlayerPrefs.SetFloat(key, floatValue);
                break;

            case string stringValue:
                PlayerPrefs.SetString(key, stringValue);
                break;

            case List<int> listValue:
                string listAsString = string.Join(",", listValue);
                PlayerPrefs.SetString(key, listAsString);
                break;

            default:
                Debug.LogWarning($"지원하지 않는 타입: {value.GetType()}");
                return;
        }

        PlayerPrefs.Save();
    }

    private static class PlayerDatasKeys
    {
        public const string level = "PlayerLevel";
        public const string currentExp = "CurrentExp";
        public const string totalSkillPoint = "TotalSkillPoint";
        public const string leftSkillPoint = "LeftSkillPoint";
        public const string skillLevels = "SkillLevels"; // List 저장용
    }
    private static class PlayerDatasDefaults
    {
        public const int level = 1;
        public const int currentExp = 0;
        public const int totalSkillPoint = 0;
        public const int leftSkillPoint = 0;
        public static readonly List<int> SkillLevels = CreateSkillLevels(Enum.GetValues(typeof(Defines.Skill)).Length);

        private static List<int> CreateSkillLevels(int count)
        {
            List<int> list = Enumerable.Repeat(0, count).ToList();
            if (count > 0)
            {
                list[0] = 1;
            }
            return list;
        }
    }

}
