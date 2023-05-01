using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
    public class TurrentManager : MonoSingleton<TurrentManager>
    {
        [SerializeField] private Turrent[] turrents;

        public Turrent GetTurrent(int id)
        {
            return turrents[id];
        }
    }
}