using System;
using UnityEngine;

namespace Levels {
    [Serializable]
    public class Level {
        public Sprite LevelBG;
        public Sprite Hero;
        public AudioClip Music;
        public GameObject LevelPrefab;
    }
}