using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
    public class MonsterManager : MonoSingleton<MonsterManager>
    {
        [Header("Reference")]
        [SerializeField] private Monster[] monster;
        [SerializeField] private Transform spawnPoint;
        [Header("Configs")]
        [SerializeField] private int amount = 10;
        [SerializeField] private float delayRound = 60f;
        [SerializeField] private float delayUnit = 0.25f;
        [SerializeField] private float multiplier = 0.5f;
        private int round = 0;
        public float Multiplier => multiplier * (float)round;

        private void Start()
        {
            StartCoroutine(OnSpawnMonster());
        }

        private IEnumerator OnSpawnMonster()
        {
            float nextTime = Time.time + delayRound;
            while (true)
            {
                int random = Random.Range(0, monster.Length);
                for (int i = 0; i < amount; i++)
                {
                    Monster newMonster = Instantiate(monster[random]);
                    newMonster.transform.position = spawnPoint.position;
                    yield return new WaitForSeconds(delayUnit);
                }
                while (Time.time < nextTime) yield return null;
                nextTime = Time.time + delayRound;
                round++;
            }
        }
    }
}