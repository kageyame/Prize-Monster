using System.Collections.Generic;
using UnityEngine;
using PrizeMonster.Data;

namespace PrizeMonster.Gameplay.Battle
{
    public static class WeightedPicker
    {
        public static SkillData PickSkill(MonsterData monster, int level)
        {
            if (monster == null || monster.Skills == null || monster.Skills.Length == 0) return null;

            // 候補を集める
            var candidates = new List<(SkillData skill, float w)>();
            float total = 0f;

            foreach (var slot in monster.Skills)
            {
                if (slot == null || slot.skill == null) continue;

                int unlock = Mathf.Max(slot.unlockLevel, slot.skill.UnlockLevel);
                if (level < unlock) continue;

                float w = Mathf.Max(0f, slot.weight) * Mathf.Max(0f, slot.skill.Weight);
                if (w <= 0f) continue;

                candidates.Add((slot.skill, w));
                total += w;
            }

            if (candidates.Count == 0 || total <= 0f) return null;

            // 抽選
            float r = Random.value * total;
            float acc = 0f;

            for (int i = 0; i < candidates.Count; i++)
            {
                acc += candidates[i].w;
                if (r <= acc)
                    return candidates[i].skill;
            }

            return candidates[candidates.Count - 1].skill;
        }
    }
}
