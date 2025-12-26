using System;
using System.Collections.Generic;
using UnityEngine;
using PrizeMonster.Data;

namespace PrizeMonster.Gameplay.Battle
{
    public sealed class BattleUnit
    {
        public MonsterData Data { get; }
        public int Level { get; }
        public string Name => Data != null ? Data.DisplayName : "Unknown";

        public int MaxHp { get; }
        public int CurrentHp { get; private set; }

        public float CooldownSec => Data.ActionCooldownSec;

        private readonly List<ActiveBuff> _buffs = new();

        public BattleUnit(MonsterData data, int level)
        {
            Data = data;
            Level = Mathf.Max(1, level);

            MaxHp = Data.HpAt(Level);
            CurrentHp = MaxHp;
        }

        public bool IsDead => CurrentHp <= 0;

        public int GetAttack()
        {
            CleanupExpiredBuffs();
            return Data.AttackAt(Level) + GetBuffSum(StatType.Attack);
        }

        public int GetDefense()
        {
            CleanupExpiredBuffs();
            return Data.DefenseAt(Level) + GetBuffSum(StatType.Defense);
        }

        public int GetHeal()
        {
            CleanupExpiredBuffs();
            return Data.HealAt(Level) + GetBuffSum(StatType.Heal);
        }

        public void TakeDamage(int amount)
        {
            amount = Mathf.Max(0, amount);
            CurrentHp = Mathf.Max(0, CurrentHp - amount);
        }

        public int HealHp(int amount)
        {
            amount = Mathf.Max(0, amount);
            int before = CurrentHp;
            CurrentHp = Mathf.Min(MaxHp, CurrentHp + amount);
            return CurrentHp - before;
        }

        public void AddBuff(StatType stat, int amount, float durationSec)
        {
            if (durationSec <= 0f || amount == 0) return;
            _buffs.Add(new ActiveBuff(stat, amount, Time.time + durationSec));
        }

        private int GetBuffSum(StatType stat)
        {
            int sum = 0;
            for (int i = 0; i < _buffs.Count; i++)
            {
                if (_buffs[i].Stat == stat && !_buffs[i].IsExpired)
                    sum += _buffs[i].Amount;
            }
            return sum;
        }

        private void CleanupExpiredBuffs()
        {
            for (int i = _buffs.Count - 1; i >= 0; i--)
            {
                if (_buffs[i].IsExpired)
                    _buffs.RemoveAt(i);
            }
        }

        private readonly struct ActiveBuff
        {
            public StatType Stat { get; }
            public int Amount { get; }
            public float ExpireAt { get; }
            public bool IsExpired => Time.time >= ExpireAt;

            public ActiveBuff(StatType stat, int amount, float expireAt)
            {
                Stat = stat;
                Amount = amount;
                ExpireAt = expireAt;
            }
        }
    }
}
