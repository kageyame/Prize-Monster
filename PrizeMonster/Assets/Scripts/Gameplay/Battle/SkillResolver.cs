using UnityEngine;
using PrizeMonster.Data;

namespace PrizeMonster.Gameplay.Battle
{
    public static class SkillResolver
    {
        public static string Execute(SkillData skill, BattleUnit actor, BattleUnit enemy)
        {
            if (skill == null || actor == null || enemy == null) return "Invalid skill/units";

            switch (skill.Category)
            {
                case SkillCategory.Attack:
                {
                    int atk = actor.GetAttack();
                    int raw = skill.FlatPower + Mathf.RoundToInt(atk * skill.StatRate);

                    int def = enemy.GetDefense();
                    int dmg = Mathf.Max(1, raw - def); // 最小1

                    enemy.TakeDamage(dmg);
                    return $"{actor.Name} uses {skill.DisplayName} => {enemy.Name} takes {dmg} dmg ({enemy.CurrentHp}/{enemy.MaxHp})";
                }

                case SkillCategory.Heal:
                {
                    int healStat = actor.GetHeal();
                    int raw = skill.FlatPower + Mathf.RoundToInt(healStat * skill.StatRate);

                    int healed = actor.HealHp(raw);
                    return $"{actor.Name} uses {skill.DisplayName} => heals {healed} ({actor.CurrentHp}/{actor.MaxHp})";
                }

                case SkillCategory.Defense:
                case SkillCategory.Buff:
                {
                    // 防御/バフは同じ扱い：自己強化（最低限）
                    actor.AddBuff(skill.BuffStat, skill.BuffAmount, skill.BuffDurationSec);
                    return $"{actor.Name} uses {skill.DisplayName} => {skill.BuffStat}+{skill.BuffAmount} for {skill.BuffDurationSec:0.#}s";
                }

                default:
                    return $"{actor.Name} does nothing";
            }
        }
    }
}
