using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private Type type;
        [SerializeField] private float hp;
        [SerializeField] private float speed;
        private MonsterMovement monsterMovement;
        private MonsterHealth monsterHealth;
        private BoxCollider boxCollider;
        public MonsterHealth MonsterHealth => monsterHealth;
        private Coroutine SlowCoroutine;

        private void Awake()
        {
            monsterMovement = GetComponent<MonsterMovement>();
            monsterHealth = GetComponent<MonsterHealth>();
            boxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            ApplyStatus();
            monsterMovement.OnFinishMove = () => Destroy(gameObject);
        }

        private void ApplyStatus()
        {
            monsterMovement.SetSpeed(speed + (speed * (MonsterManager.I.Multiplier / 100f)));
            monsterHealth.SetHealth(hp + (hp * (MonsterManager.I.Multiplier / 100f)));
        }

        public void ApplyDamage(Turrent damager)
        {
            float damage = Random.Range(damager.MinDamage, damager.MaxDamage);
            if (damager.Type == type) damage += (damage * (50f / 100f));
            monsterHealth.ApplyDamage(damage);
            if (damager.DamageEffects != null)
            {
                foreach (var damageEffect in damager.DamageEffects)
                {
                    switch (damageEffect.Type)
                    {
                        case EffectType.Slow:
                            SlowCoroutine ??= StartCoroutine(OnSlow(damageEffect));
                            break;
                    }
                }
            }
            GameObject effect = Instantiate(damager.HitVfx);
            effect.transform.SetParent(transform);
            Vector3 pos = transform.position;
            pos.y += boxCollider.bounds.size.y;
            effect.transform.position = pos;
        }

        private IEnumerator OnSlow(DamageEffect damageEffect)
        {
            monsterMovement.SetSpeed(speed - (speed * (damageEffect.Value / 100f)));
            yield return new WaitForSeconds(damageEffect.Duration);
            monsterMovement.SetSpeed(speed);
            SlowCoroutine = null;
        }
    }
}