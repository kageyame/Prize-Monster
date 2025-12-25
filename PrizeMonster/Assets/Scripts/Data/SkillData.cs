using UnityEngine;

namespace PrizeMonster.Data
{
    public enum SkillCategory { Attack, Defense, Heal, Buff }
    public enum TargetType { Self, Enemy, Ally }
    public enum StatType { Attack, Defense, Heal }

    [CreateAssetMenu(menuName = "PrizeMonster/Data/Skill", fileName = "Skill_")]
    public class SkillData : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string id = "S000";
        [SerializeField] private string displayName = "New Skill";
        [TextArea(2, 4)]
        [SerializeField] private string description;

        [Header("Rule")]
        [SerializeField] private SkillCategory category = SkillCategory.Attack;
        [SerializeField] private TargetType target = TargetType.Enemy;
        [Tooltip("このLv以上で使用候補に入る")]
        [SerializeField] private int unlockLevel = 1;

        [Tooltip("ランダム抽選の重み（大きいほど出やすい）")]
        [SerializeField] private float weight = 1f;

        [Header("Attack/Heal Formula")]
        [Tooltip("固定値（ダメージ/回復）")]
        [SerializeField] private int flatPower = 0;

        [Tooltip("攻撃/回復の参照ステータス倍率（例：0.8 => Atk*0.8を加算）")]
        [SerializeField] private float statRate = 1f;

        [Header("Buff/Defense")]
        [SerializeField] private StatType buffStat = StatType.Attack;
        [SerializeField] private int buffAmount = 0;
        [Tooltip("秒。0ならバフ無し")]
        [SerializeField] private float buffDurationSec = 0f;

        public string Id => id;
        public string DisplayName => displayName;
        public string Description => description;
        public SkillCategory Category => category;
        public TargetType Target => target;
        public int UnlockLevel => unlockLevel;
        public float Weight => weight;

        public int FlatPower => flatPower;
        public float StatRate => statRate;

        public StatType BuffStat => buffStat;
        public int BuffAmount => buffAmount;
        public float BuffDurationSec => buffDurationSec;

        // 計算はバトル側でやる。ここは「式の材料」を持つだけにする。
    }
}
