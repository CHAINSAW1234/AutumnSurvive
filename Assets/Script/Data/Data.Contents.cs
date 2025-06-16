using System.Collections.Generic;
using static Defines;

namespace Data
{
    [System.Serializable]
    public struct Direction
    {
        public float x;
        public float y;

        // Vector2로 변환하고 싶을 경우 추가 메서드
        public UnityEngine.Vector2 ToVector2() => new UnityEngine.Vector2(x, y);
    }

    #region Skill

    [System.Serializable]
    public struct SkillLevelInfo
    {
        public int level;
        public float moveSpeed;
        public Direction direction;
        public float duration;
        public int createCount;
    }

    [System.Serializable]
    public struct SkillLevelData
    {
        public Defines.Skill Skill; // not used, but in jsonFile
        public List<SkillLevelInfo> SkillData;
        
        public readonly int MaxLevel { get => SkillData.Count; }
        public readonly SkillLevelInfo? this[int i]
        {
            get
            {
                return SkillData.Find(data => data.level == i);
            }
        }

    }

    #endregion

    #region Level

    [System.Serializable]
    public struct PlayerLevelInfo
    {
        public int level;
        public int exp;
        public bool skillPoint;
        public Defines.Skill skillUnlock;
    }

    [System.Serializable]
    public struct PlayerLevelData
    {
        public List<PlayerLevelInfo> LevelData;

        public Dictionary<int, PlayerLevelInfo> MakeDict()
        {
            Dictionary<int, PlayerLevelInfo> dict = new Dictionary<int, PlayerLevelInfo>();

            foreach(PlayerLevelInfo info in LevelData)
            {
                dict.Add(info.level, info);
            }

            return dict;
        }
    }

    #endregion
}