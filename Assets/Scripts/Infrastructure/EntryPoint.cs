using Infrastructure.Input;
using Levels;
using ScriptableObjects;
using UnityEngine;

namespace Infrastructure {
    public class EntryPoint : MonoBehaviour {
        [SerializeField] private HeroSettings _heroSettings;
        [SerializeField] private ScriptableObjects.Levels _levels;
        
        [SerializeField] private ComputerInputSystem _inputSystem;
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private LevelMover _levelMover;
        [SerializeField] private TextGenerator.TextGenerator _textGenerator;

        [SerializeField] private Vector2 _startPosition;

        private void Start() {
            _textGenerator.Init();
            _levelLoader.Init(_heroSettings, _startPosition, _inputSystem, _levelMover, _textGenerator);
            _levelLoader.RunLevel(_levels.LevelPrefabs[0]);
        }

        
    }
}