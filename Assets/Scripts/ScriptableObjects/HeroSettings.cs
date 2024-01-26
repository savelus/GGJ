using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "Hero", menuName = "Hero")]
    public class HeroSettings : ScriptableObject {
        public Hero.Hero heroPrefab;
        
        [Range(1,89)] public int Angle;
        public int Speed;
    }
}