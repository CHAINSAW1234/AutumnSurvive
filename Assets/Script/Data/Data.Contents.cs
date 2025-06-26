using System.Collections.Generic;
using static Defines;

namespace Data
{

    #region Skill

    [System.Serializable]
    public struct SkillLevelInfo
    {
        public int level;
        public float moveSpeed;
        public float duration;
        public int createCount;

        public struct InfoDescription
        {
            public const string level = "레벨";
            public const string moveSpeed = "속도";
            public const string duration = "지속 시간";
            public const string createCount = "생성 개수";
        }
    }

    [System.Serializable]
    public struct SkillLevelData
    {
        public Defines.Skill Skill; // not used, but in jsonFile
        public List<SkillLevelInfo> SkillData;
        public string Description;

        public readonly int MaxLevel { get => SkillData.Count; }
        public readonly SkillLevelInfo? this[int i]
        {
            get
            {
                return SkillData.Find(data => data.level == i);
            }
        }

        public readonly string GetLevelInfo()
        {
            var levelData = Managers.Data.GetSkillLevelDataHelper(Skill, PlayerDataController.Instance.GetSkillLevelAt(Skill)).Value;
            List<string> infoParts = new();

            void AddInfo(string description, object value)
            {
                infoParts.Add($"{description} : {value}");
            }

            switch (Skill)
            {
                case Defines.Skill.ButterflyExplosion:
                    AddInfo(SkillLevelInfo.InfoDescription.duration, levelData.duration);
                    break;

                case Defines.Skill.Squirrals:
                case Defines.Skill.ShieldBee:
                    AddInfo(SkillLevelInfo.InfoDescription.createCount, levelData.createCount);
                    AddInfo(SkillLevelInfo.InfoDescription.duration, levelData.duration);
                    break;

                case Defines.Skill.Butterfly:
                    AddInfo(SkillLevelInfo.InfoDescription.moveSpeed, levelData.moveSpeed);
                    break;

                case Defines.Skill.SpreadBeaver:
                case Defines.Skill.StraightBeaver:
                case Defines.Skill.StraightBee:
                    AddInfo(SkillLevelInfo.InfoDescription.createCount, levelData.createCount);
                    break;
            }

            return string.Join(", ", infoParts);
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