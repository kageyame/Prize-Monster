using UnityEngine;

namespace PrizeMonster.Data
{
    public enum PrizeType
    {
        ExpOrb,
        LevelUpOrb,
        StatOrb,
        CharacterOrb,
        WeightTrigger // ★重りギミック用（落とすと経験値シャワーを呼ぶ）
    }

    [CreateAssetMenu(menuName = "PrizeMonster/Data/Prize", fileName = "Prize_")]
    public class PrizeData : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string id = "P000";
        [SerializeField] private string displayName = "New Prize";
        [SerializeField] private PrizeType type = PrizeType.ExpOrb;
        [SerializeField] private Sprite icon;

        [Header("Spawn")]
        [Tooltip("流れてくるプライズ抽選の重み（大きいほど出やすい）")]
        [SerializeField] private float weight = 1f;

        [Header("Exp/LevelUp")]
        [SerializeField] private int expAmount = 0;
        [SerializeField] private int levelUpAmount = 0;

        [Header("Stat Orb")]
        [SerializeField] private StatType statType = StatType.Attack;
        [SerializeField] private int buffAmount = 0;
        [SerializeField] private float durationSec = 10f;

        [Header("Character Orb")]
        [Tooltip("順2/順3を戦線投入できる（ゲーム側で誰を出すか決める）")]
        [SerializeField] private bool enablesReinforcement = false;

        public string Id => id;
        public string DisplayName => displayName;
        public PrizeType Type => type;
        public Sprite Icon => icon;
        public float Weight => weight;

        public int ExpAmount => expAmount;
        public int LevelUpAmount => levelUpAmount;

        public StatType StatType => statType;
        public int BuffAmount => buffAmount;
        public float DurationSec => durationSec;

        public bool EnablesReinforcement => enablesReinforcement;
    }
}
