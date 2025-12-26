using System.Collections;
using UnityEngine;
using PrizeMonster.Data;

namespace PrizeMonster.Gameplay.Battle
{
    public class BattleRunner : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private MonsterData playerMonster;
        [SerializeField] private int playerLevel = 1;

        [SerializeField] private MonsterData enemyMonster;
        [SerializeField] private int enemyLevel = 1;

        [Header("Debug")]
        [SerializeField] private bool autoStart = true;

        private BattleUnit _player;
        private BattleUnit _enemy;
        private bool _battleEnded;

        private void Start()
        {
            if (autoStart)
                StartBattle();
        }

        [ContextMenu("StartBattle")]
        public void StartBattle()
        {
            if (playerMonster == null || enemyMonster == null)
            {
                Debug.LogError("Assign playerMonster and enemyMonster in Inspector.");
                return;
            }

            _player = new BattleUnit(playerMonster, playerLevel);
            _enemy  = new BattleUnit(enemyMonster, enemyLevel);
            _battleEnded = false;

            Debug.Log($"BATTLE START: {_player.Name}(Lv{playerLevel}) vs {_enemy.Name}(Lv{enemyLevel})");
            Debug.Log($"{_player.Name} HP {_player.CurrentHp}/{_player.MaxHp} | {_enemy.Name} HP {_enemy.CurrentHp}/{_enemy.MaxHp}");

            StartCoroutine(UnitLoop(_player, _enemy));
            StartCoroutine(UnitLoop(_enemy, _player));
        }

        private IEnumerator UnitLoop(BattleUnit actor, BattleUnit enemy)
        {
            // 開始直後に同時発動しすぎるので少しズラす
            yield return new WaitForSeconds(Random.Range(0.05f, 0.25f));

            while (!_battleEnded)
            {
                if (actor.IsDead || enemy.IsDead)
                {
                    EndBattle();
                    yield break;
                }

                yield return new WaitForSeconds(Mathf.Max(0.1f, actor.CooldownSec));

                if (_battleEnded) yield break;
                if (actor.IsDead || enemy.IsDead)
                {
                    EndBattle();
                    yield break;
                }

                var skill = WeightedPicker.PickSkill(actor.Data, actor.Level);
                if (skill == null)
                {
                    Debug.LogWarning($"{actor.Name} has no usable skills.");
                    continue;
                }

                string log = SkillResolver.Execute(skill, actor, enemy);
                Debug.Log(log);

                if (actor.IsDead || enemy.IsDead)
                {
                    EndBattle();
                    yield break;
                }
            }
        }

        private void EndBattle()
        {
            if (_battleEnded) return;
            _battleEnded = true;

            string result;
            if (_player.IsDead && _enemy.IsDead) result = "DRAW";
            else if (_enemy.IsDead) result = "PLAYER WIN";
            else if (_player.IsDead) result = "ENEMY WIN";
            else result = "ENDED";

            Debug.Log($"BATTLE END: {result}");
        }
    }
}
