using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Data
{
    // JSON

    #region PlayerData(JSON)
    [Serializable]
    public class PlayerData
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class PlayerDataLoader : ILoader<int, PlayerData>
    {
        public List<PlayerData> stats = new List<PlayerData>();

        public Dictionary<int, PlayerData> MakeDict()
        {
            Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
            foreach (PlayerData stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }

    #endregion

    #region CreatureData

    [Serializable]
    public class CreatureData
    {
        public int DataId;

        public string Name;
        public string Prefab;
        public float MaxHp;
        public float Atk;
        public float MoveSpeed;
        public float TotalExp;
    }

    [Serializable]
    public class CreatureDataLoader : ILoader<int, CreatureData>
    {
        public List<CreatureData> creatures = new List<CreatureData>();

        public Dictionary<int, CreatureData> MakeDict()
        {
            Dictionary<int, CreatureData> dict = new Dictionary<int, CreatureData>();
            foreach (CreatureData creature in creatures)
                dict.Add(creature.DataId, creature);
            return dict;
        }
    }

    #endregion

    #region SkillData(JSON)

    [Serializable]
    public class SkillData
    {
        public int templateID;

        public string name;
        public string type;
        public string prefab;
        public int damage;
    }

    [Serializable]
    public class SkillDataLoader : ILoader<int, SkillData>
    {
        public List<SkillData> skills = new List<SkillData>();

        public Dictionary<int, SkillData> MakeDict()
        {
            Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();
            foreach (SkillData skill in skills)
                dict.Add(skill.templateID, skill);
            return dict;
        }
    }

    #endregion


    // xml

    #region PlayerData(XML)
    //public class PlayerData
    //{
    //    [XmlAttribute]
    //    public int level;
    //    [XmlAttribute]
    //    public int maxHp;
    //    [XmlAttribute]
    //    public int attack;
    //    [XmlAttribute]
    //    public int totalExp;
    //}

    //[Serializable, XmlRoot("PlayerDatas")]
    //public class PlayerDataLoader : ILoader<int, PlayerData>
    //{
    //    [XmlElement("PlayerData")]
    //    public List<PlayerData> stats = new List<PlayerData>();

    //    public Dictionary<int, PlayerData> MakeDict()
    //    {
    //        Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
    //        foreach (PlayerData stat in stats)
    //            dict.Add(stat.level, stat);
    //        return dict;
    //    }
    //}

    #endregion

    #region MonsterData

    //public class MonsterData
    //{
    //    [XmlAttribute]
    //    public string name;
    //    [XmlAttribute]
    //    public string prefab;
    //    [XmlAttribute]
    //    public int level;
    //    [XmlAttribute]
    //    public int maxHp;
    //    [XmlAttribute]
    //    public int attack;
    //    [XmlAttribute]
    //    public float speed;

    //    // DropData
    //    // - 일정 확률로
    //    // - 어떤 아이템을(보석, 스킬 가챠, 골드, 고기)
    //    // - 몇개 드롭?
    //}

    #endregion

    #region DropItemData

    public class DropItemData
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string type;
        [XmlAttribute]
        public int exp;
    }

    #endregion

    #region SkillData

    [Serializable]
    public class HitEffect
    {
        [XmlAttribute]
        public string type;
        [XmlAttribute]
        public int templateID;
        [XmlAttribute]
        public int value;
    }

    //public class SkillData
    //{
    //    [XmlAttribute]
    //    public int templateID;

    //    //[XmlAttribute(AttributeName="type")]
    //    //public string skillTypeStr;
    //    //public Define.SkillType skillType = Define.SkillType.None;

    //    [XmlAttribute]
    //    public int nextID;
    //    public int prevID = 0;

    //    [XmlAttribute]
    //    public string prefab;

    //    // 아주 많이
    //    [XmlAttribute]
    //    public int damage;

    //    //[XmlElement("HitEffect")]
    //    //public List<HitEffect> hitEffects = new List<HitEffect>();
    //}

    //[Serializable, XmlRoot("SkillDatas")]
    //public class SkillDataLoader : ILoader<int, SkillData>
    //{
    //    [XmlElement("SkillData")]
    //    public List<SkillData> skills = new List<SkillData>();

    //    public Dictionary<int, SkillData> MakeDict()
    //    {
    //        Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();
    //        foreach (SkillData skill in skills)
    //            dict.Add(skill.templateID, skill);
    //        return dict;
    //    }
    //}

    #endregion
}
