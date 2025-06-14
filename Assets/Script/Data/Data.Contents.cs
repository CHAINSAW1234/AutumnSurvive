using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using static Defines;

namespace Data
{
    [System.Serializable]
    public struct Direction
    {
        public float x;
        public float y;

        // Vector2로 변환하고 싶을 경우 추가 메서드
        public Vector2 ToVector2() => new Vector2(x, y);
    }


    #region Skill

    [System.Serializable]
    public struct SkillLevelData
    {
        public int level;
        public float moveSpeed;
        public Direction direction;
        public float duration;
        public int createCount;
    }

    [System.Serializable]
    public struct SkillDefinition : ILoader<int, SkillLevelData>
    {
        public Defines.Skill Skill; // not used, but in jsonFile
        public List<SkillLevelData> SkillData;

        public Dictionary<int, SkillLevelData> MakeDict()
        {
            Dictionary<int, SkillLevelData> dict = new Dictionary<int, SkillLevelData>();

            foreach (SkillLevelData data in SkillData)  
            {
                dict.Add(data.level, data);

                if (data.duration == -1)
                {
                    SkillLevelData skillLevelData = dict[data.level];
                    skillLevelData.duration = Inf;
                }
            }

            return dict;
        }
    }

    #endregion
}