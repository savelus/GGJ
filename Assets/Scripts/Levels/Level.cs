using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Levels {
    [Serializable]
    public class Level {
        public LevelType LevelName;
        public Sprite LevelBG;
        public Sprite Hero;
        public AudioClip Music;
        public GameObject LevelPrefab;
    }
}