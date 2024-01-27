using System.Collections.Generic;
using Levels;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "LevelsConfig")]
    public class Levels : ScriptableObject {
        public List<Level> LevelsSettings;
    }
}