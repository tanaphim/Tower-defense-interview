using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace TowerDefense
{
    public class PathManager : MonoSingleton<PathManager>
    {
        public Transform[] Paths;
    }
}