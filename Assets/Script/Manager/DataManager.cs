using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<Defines.Skill, SkillLevelData> SkillDict { get; private set; } = new Dictionary<Defines.Skill, SkillLevelData>();
    public Dictionary<int, PlayerLevelInfo> PlayerLevelDict { get; private set; } = new Dictionary<int, PlayerLevelInfo>();

    public int MaxLevel { get => PlayerLevelDict.Count; }

    public void Init()
    {
        ReadSkillDatas();
        PlayerLevelData playerleveldata = LoadJson<PlayerLevelData>("PlayerLevelData");
        PlayerLevelDict = playerleveldata.MakeDict();
    }

    public SkillLevelInfo? GetSkillLevelDataHelper(Defines.Skill skill, int level)
    {
        if (SkillDict.ContainsKey(skill))
        {
            return SkillDict[skill][level];
        }

        return null;
    }

    T LoadJson<T>(string path)
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<T>(textAsset.text);
    }

    private void ReadSkillDatas()
    {
        foreach (Enum skill in Enum.GetValues(typeof(Defines.Skill)))
        {
            SkillLevelData skillLoader = LoadJson<SkillLevelData>(skill.ToDescription());
            SkillDict[(Defines.Skill)skill] = skillLoader;
        }
    }

    public void Clear()
    {
        SkillDict.Clear();
    }
}
