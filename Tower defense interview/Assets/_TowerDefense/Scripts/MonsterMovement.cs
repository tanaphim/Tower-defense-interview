using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
namespace TowerDefense
{
    public class MonsterMovement : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private int nextPath = 0;
        private const float DistanceToWaypointThreshold = 0.01f;
        public Action OnFinishMove;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            StartCoroutine(OnMove());
        }

        public void SetSpeed(float speed)
        {
            navMeshAgent.speed = speed;
        }

        private IEnumerator OnMove()
        {
            while (nextPath < PathManager.I.Paths.Length)
            {
                navMeshAgent.SetDestination(PathManager.I.Paths[nextPath].position);
                yield return new WaitUntil(() => IsNextPath());
                nextPath++;
            }
            OnFinishMove?.Invoke();

            bool IsNextPath()
            {
                float distance = Vector3.Distance(transform.position, PathManager.I.Paths[nextPath].position);
                bool isNextPath = distance <= DistanceToWaypointThreshold;
                return isNextPath;
            }
        }
    }
}