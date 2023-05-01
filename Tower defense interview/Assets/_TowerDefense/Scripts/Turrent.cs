using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
    public class Turrent : MonoBehaviour
    {
        public Type Type;
        public float Range;
        public float FireRate;
        public float MinDamage;
        public float MaxDamage;
        public DamageType DamageType;
        public float DamageArea;
        public DamageEffect[] DamageEffects;
        public TargetPriority[] TargetPrioritys;
        public GameObject HitVfx;

    }
}