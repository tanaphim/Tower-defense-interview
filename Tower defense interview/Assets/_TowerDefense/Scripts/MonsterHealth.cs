using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
    public class MonsterHealth : MonoBehaviour
    {
        private float currentHealth, maxHealth;
        public float CurrentHealth => currentHealth;

        public void SetHealth(float health)
        {
            currentHealth = health;
            maxHealth = health;
        }

        public void ApplyDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (currentHealth == 0) Destroy(gameObject);
        }
    }
}