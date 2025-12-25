using System;
using UnityEngine;

namespace PrizeMonster.Data
{
    [Serializable]
    public class SkillSlot
    {
        public SkillData skill;
        [Min(0f)] public float weight = 1f;
        [Min(1)] public int unlockLevel = 1;
    }

    [CreateAssetMenu(menuName = "PrizeMonster/Data/Monster", fileName = "Monster_")]
    public class MonsterData : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string id = "M000";
        [SerializeField] private string displayName = "New Monster";

        [Header("Level")]
        [SerializeField] private int maxLevel = 50;

        [Header("Base Stats (Lv1)")]
        [SerializeField] private int baseAttack = 10;
        [SerializeField] private int baseDefense = 10;
        [SerializeField] private int baseHeal = 10;

        [Header("Growth per Level")]
        [SerializeField] private int growthAttack = 2;
        [SerializeField] private int growthDefense = 2;
        [SerializeField] private int growthHeal = 2;

        [Header("Battle")]
        [Tooltip("行動クールタイム（秒）。短いほど手数が多い")]
        [SerializeField] private float actionCooldownSec = 3.0f;

        [SerializeField] private SkillSlot[] skills;

        public string Id => id;
        public string DisplayName => displayName;
        public int MaxLevel => maxLevel;
        public float ActionCooldownSec => actionCooldownSec;
        public SkillSlot[] Skills => skills;

        public int AttackAt(int level) => baseAttack + Mathf.Max(0, level - 1) * growthAttack;
        public int DefenseAt(int level) => baseDefense + Mathf.Max(0, level - 1) * growthDefense;
        public int HealAt(int level) => baseHeal + Mathf.Max(0, level - 1) * growthHeal;
    }
}
