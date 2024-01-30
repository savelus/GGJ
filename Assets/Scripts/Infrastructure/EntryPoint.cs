using Infrastructure.Input;
using Levels;
using ScriptableObjects;
using UI;
using UI.Menu;
using UnityEngine;

namespace Infrastructure {
    public class EntryPoint : MonoBehaviour {
        [SerializeField] private HeroSettings _heroSettings;
        [SerializeField] private ScriptableObjects.Levels _levels;
        
        [SerializeField] private ComputerInputSystem _inputSystem;
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private LevelMover _levelMover;
        [SerializeField] private TextGenerator.TextGenerator _textGenerator;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private ScoreController.ScoreController _scoreController;
        [SerializeField] private AudioManager _audioManager;

        [SerializeField] private Vector2 _startPosition;
        private int _score;
        private LevelType _currentLevel;

        private void Start() {
            _mainMenu.SubscribeOnStartButton(StartLevel);
            _mainMenu.gameObject.SetActive(true);
            _mainMenu.ViewAllRecords();
            
            _textGenerator.Init();
            _levelLoader.Init(_heroSettings, _startPosition, _inputSystem, _levelMover, _textGenerator, _scoreController, _audioManager);
            _levelLoader.SubscribeOnLevelEnd(EndLevel);
        }

        private void StartLevel() {
            _mainMenu.gameObject.SetActive(false);
            var level = GetRandomLevel();
            _currentLevel = level.LevelName;
            _levelLoader.RunLevel(level, () => _scoreController.StartScore());
            _scoreController.SubscribeOnScoreChanged(ScoreChanged);
            _gameUI.SetScore(0);
            _gameUI.SetBackground(level.LevelBG);
            _audioManager.PlayBackSound(level.Music);
        }

        private void EndLevel() {
            _mainMenu.gameObject.SetActive(true);
            _scoreController.EndScore();
            SetScore();
            _audioManager.StopBackSound();
        }

        private void SetScore() {
            
            if (PlayerPrefs.GetInt(_currentLevel.ToString()) > _score) return;

            _mainMenu.SetRecord(_currentLevel, _score);
            PlayerPrefs.SetInt(_currentLevel.ToString(), _score);
        }

        private void ScoreChanged(int score) {
            _score = score;
            _gameUI.SetScore(score);
        }

        private Level GetRandomLevel() {
            return _levels.LevelsSettings[Random.Range(0, _levels.LevelsSettings.Count)];
        }
    }
}