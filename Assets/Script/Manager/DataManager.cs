using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<Defines.Skill, Dictionary<int, SkillLevelData>> SkillDict { get; private set; } = new Dictionary<Defines.Skill, Dictionary<int, SkillLevelData>>();
    
    public void Init()
    {
        ReadSkillDatas();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    private void ReadSkillDatas()
    {
        foreach (Enum skill in Enum.GetValues(typeof(Defines.Skill)))
        {
            SkillDefinition skillLoader = LoadJson<SkillDefinition, int, SkillLevelData>(skill.ToDescription());
            SkillDict[(Defines.Skill)skill] = skillLoader.MakeDict();
        }
    }

    SkillLevelData? GetSkillLevelDataHelper(Defines.Skill skill, int level)
    {
        if (SkillDict.ContainsKey(skill) && SkillDict[skill].ContainsKey(level))
        {
            return SkillDict[skill][level];
        }

        return null;
    }

    public void Clear()
    {
        foreach (var innerDict in SkillDict.Values)
        {
            innerDict.Clear();
        }
        SkillDict.Clear();
    }
}
