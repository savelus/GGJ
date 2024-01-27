using System.Collections.Generic;
using Levels;
using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "LevelsConfig")]
    public class Levels : ScriptableObject {
        public List<GameObject> LevelPrefabs;
    }
}