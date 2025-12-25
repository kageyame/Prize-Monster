using UnityEngine;

namespace PrizeMonster.Data
{
    [CreateAssetMenu(menuName = "PrizeMonster/Data/GameDatabase", fileName = "GameDatabase")]
    public class GameDatabase : ScriptableObject
    {
        [SerializeField] private MonsterData[] monsters;
        [SerializeField] private SkillData[] skills;
        [SerializeField] private PrizeData[] prizes;

        public MonsterData[] Monsters => monsters;
        public SkillData[] Skills => skills;
        public PrizeData[] Prizes => prizes;
    }
}
