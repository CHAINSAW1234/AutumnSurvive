using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    private static PlayerDataController s_Instance = null;
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
            s_Instance.LoadAllPlayerData();
        }
    }

    private int level;
    private int currentExp;
    private int totalSkillPoint;
    private int leftSkillPoint;
    private readonly List<int> skillLevels = PlayerDatasDefaults.skillLevels;//PlayerDatasDefaults.CreateSkillLevels(Enum.GetValues(typeof(Defines.Skill)).Length);
    private readonly List<float> sounds = PlayerDatasDefaults.sounds; //Enumerable.Repeat(100f, Enum.GetValues(typeof(Defines.Sound)).Length).ToList();

    public int Level { get => level; set { level = value; SavePlayerData(PlayerDatasKeys.level, level); } }
    public int CurrentExp { get => currentExp; set { currentExp = value; SavePlayerData(PlayerDatasKeys.currentExp, currentExp); } }
    public int TotalSkillPoint { get => totalSkillPoint; set { totalSkillPoint = value; SavePlayerData(PlayerDatasKeys.totalSkillPoint, totalSkillPoint); } }
    public int LeftSkillPoint { get => leftSkillPoint; set { leftSkillPoint = value; SavePlayerData(PlayerDatasKeys.leftSkillPoint, leftSkillPoint); } }
    public ReadOnlyCollection<int> SkillLevels { get => skillLevels.AsReadOnly(); }
    public ReadOnlyCollection<float> Sounds { get => sounds.AsReadOnly(); }
   
    private void OnDestroy()
    {
        SaveAllPlayerData();
    }

    public Defines.Skill GetRandomEnableSkill()
    {
        // 1. 값이 0이 아닌 인덱스들을 수집
        List<int> nonZeroIndices = new List<int>();
        for (int i = 0; i < SkillLevels.Count; i++)
        {
            if (SkillLevels[i] != 0)
                nonZeroIndices.Add(i);
        }

        // 3. 무작위 인덱스 반환
        int randomIndex = UnityEngine.Random.Range(0, nonZeroIndices.Count);
        return (Defines.Skill)randomIndex;
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

    public float GetSoundAt(Defines.Sound sound)
    {
        return sounds[(int)sound];
    }
    public void SetSoundAt(Defines.Sound sound, float value)
    {
        int index = (int)sound;

        value = Math.Clamp(value, 0, 100);
        sounds[index] = value;
        SavePlayerData(PlayerDatasKeys.sounds, sounds);


    }

    public void SaveAllPlayerData() {
        PlayerPrefs.SetInt(PlayerDatasKeys.level, level);
        PlayerPrefs.SetInt(PlayerDatasKeys.currentExp, currentExp);
        PlayerPrefs.SetInt(PlayerDatasKeys.totalSkillPoint, totalSkillPoint);
        PlayerPrefs.SetInt(PlayerDatasKeys.leftSkillPoint, leftSkillPoint);

        // List<int> → 문자열로 저장 (쉼표로 구분)
        string skillLevelsString = string.Join(",", skillLevels);
        PlayerPrefs.SetString(PlayerDatasKeys.skillLevels, skillLevelsString);

        string soundsString = string.Join(",", sounds);
        PlayerPrefs.SetString(PlayerDatasKeys.sounds, soundsString);

        PlayerPrefs.Save(); // 저장 적용
    }
    public void LoadAllPlayerData()
    {
        level = PlayerPrefs.GetInt(PlayerDatasKeys.level, PlayerDatasDefaults.level);
        currentExp = PlayerPrefs.GetInt(PlayerDatasKeys.currentExp, PlayerDatasDefaults.currentExp);
        totalSkillPoint = PlayerPrefs.GetInt(PlayerDatasKeys.totalSkillPoint, PlayerDatasDefaults.totalSkillPoint);
        leftSkillPoint = PlayerPrefs.GetInt(PlayerDatasKeys.leftSkillPoint, PlayerDatasDefaults.leftSkillPoint);

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

        string soundString = PlayerPrefs.GetString(PlayerDatasKeys.sounds, "");

        if (!string.IsNullOrEmpty(soundString))
        {
            string[] values = soundString.Split(',');
            for (int i = 0; i < values.Length; ++i)
            {
                if (float.TryParse(values[i], out float sound))
                {
                    sounds[i] = sound;
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
                string listIntAsString = string.Join(",", listValue);
                PlayerPrefs.SetString(key, listIntAsString);
                break;

            case List<float> listValue:
                string listFloatAsString = string.Join(",", listValue);
                PlayerPrefs.SetString(key, listFloatAsString);
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
        public const string sounds = "Sounds";
    }
    private static class PlayerDatasDefaults
    {
        public const int level = 1;
        public const int currentExp = 0;
        public const int totalSkillPoint = 0;
        public const int leftSkillPoint = 0;
        public static readonly List<int> skillLevels = CreateSkillLevels(Enum.GetValues(typeof(Defines.Skill)).Length);
        public static readonly List<float> sounds = Enumerable.Repeat(100f, Enum.GetValues(typeof(Defines.Sound)).Length).ToList();

        public static List<int> CreateSkillLevels(int count)
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
