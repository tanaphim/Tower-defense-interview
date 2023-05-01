using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace TowerDefense
{
    public class TurrentAttack : MonoBehaviour
    {
        private Turrent turrent;
        [SerializeField] private Transform orientation;
        [SerializeField] private ParticleSystem fireVfx;
        private List<Monster> Enemies = new List<Monster>();
        [SerializeField] private List<Monster> monsters = new List<Monster>();

        private void Awake()
        {
            turrent = GetComponent<Turrent>();
        }

        private void Start()
        {
            StartCoroutine(OnActivation());
        }

        private IEnumerator OnActivation()
        {
            while (true)
            {
                Monster target = FindTarget();
                if (target)
                {
                    switch (turrent.DamageType)
                    {
                        case DamageType.Single:
                            target.ApplyDamage(turrent);
                            break;
                        case DamageType.Aoe:
                            Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, turrent.DamageArea);
                            foreach (var hit in hitColliders)
                            {
                                if (hit.GetComponent<Monster>()) hit.GetComponent<Monster>().ApplyDamage(turrent);
                            }
                            break;
                    }
                    fireVfx.Play();
                }
                yield return new WaitForSeconds(turrent.FireRate);
            }
        }

        private Monster FindTarget()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, turrent.Range);
            monsters = new List<Monster>();
            foreach (var hit in hitColliders)
            {
                if (hit.GetComponent<Monster>()) monsters.Add(hit.GetComponent<Monster>());
            }
            IOrderedEnumerable<Monster> orders = null;
            if (turrent.TargetPrioritys == null && turrent.TargetPrioritys.Length > 0) return null;
            switch (turrent.TargetPrioritys[0])
            {
                case TargetPriority.Nearest:
                    orders = monsters.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));
                    break;
                case TargetPriority.Farthest:
                    orders = monsters.OrderByDescending(x => Vector3.Distance(transform.position, x.transform.position));
                    break;
                case TargetPriority.LowestHp:
                    orders = monsters.OrderBy(x => x.MonsterHealth.CurrentHealth);
                    break;
                case TargetPriority.HighestHp:
                    orders = monsters.OrderByDescending(x => x.MonsterHealth.CurrentHealth);
                    break;
            }

            for (int i = 1; i < turrent.TargetPrioritys.Length; i++)
            {
                switch (turrent.TargetPrioritys[i])
                {
                    case TargetPriority.Nearest:
                        orders = orders.ThenBy(x => Vector3.Distance(transform.position, x.transform.position));
                        break;
                    case TargetPriority.Farthest:
                        orders = orders.ThenByDescending(x => Vector3.Distance(transform.position, x.transform.position));
                        break;
                    case TargetPriority.LowestHp:
                        orders = orders.ThenBy(x => x.MonsterHealth.CurrentHealth);
                        break;
                    case TargetPriority.HighestHp:
                        orders = orders.ThenByDescending(x => x.MonsterHealth.CurrentHealth);
                        break;
                }
            }
            if (orders == null || orders.Count() == 0) return null;
            var lookPos = orders.First().transform.position - transform.position;
            lookPos.y = 0;
            orientation.rotation = Quaternion.LookRotation(lookPos);
            return orders.First();
        }
    }
}