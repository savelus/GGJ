using System;
using System.Collections;
using Infrastructure.Input;
using Levels;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;

namespace Infrastructure {
    public class LevelLoader : MonoBehaviour {
        private Vector2 _startPosition;
        private HeroSettings _heroSettings;
        private ComputerInputSystem _inputSystem;
        private LevelMover _mover;
        private TextGenerator.TextGenerator _textGenerator;

        private Hero.Hero _hero;

        private Action _onLevelEnd;
        private ScoreController.ScoreController _scoreController;
        private AudioManager _audioManager;

        public void Init(HeroSettings heroSettings, 
                         Vector2 startPosition, 
                         ComputerInputSystem inputSystem, 
                         LevelMover mover,
                         TextGenerator.TextGenerator textGenerator,
                         ScoreController.ScoreController scoreController,
                         AudioManager audioManager) {
            _audioManager = audioManager;
            _scoreController = scoreController;
            _mover = mover;
            _textGenerator = textGenerator;
            _heroSettings = heroSettings;
            SpawnHero();
             _startPosition = startPosition;

            _inputSystem = inputSystem;
            _inputSystem.SubscribeOnChangeDirection(ChangeDirection);
        }

        private void ChangeDirection() {
            _hero.ChangeDirection();
            _audioManager.ChangeDirectionEffect();
        }

        public void RunLevel(Level level, Action startScore) {
            ViewHero(level.Hero);
            _mover.Init(_heroSettings.Speed, level.LevelPrefab);
            StartCoroutine(StartLevelCoroutine(startScore));
        }

        public void EndLevel() {
            _hero.SetMoveState(false);
            SetViewHeroState(false);
            
            _mover.EndLevel();
            _textGenerator.EndLevel();
            
            _onLevelEnd?.Invoke();
        }

        private void ViewHero(Sprite heroSprite) {
            if (_hero == null) {
                print("Hero not instantiate");
                return;
            }

            _hero.Init(_heroSettings, Camera.main.orthographicSize, HeroTrigger, SpawnSymbol, heroSprite);
            _hero.transform.position = _startPosition;
            SetViewHeroState(true);
        }

        private void HeroTrigger(Collider2D obj) {
            if (obj.CompareTag("border")) {
                EndLevel();
            }
            else if (obj.CompareTag("Coin")) {
                _scoreController.AddScore(5);
                _audioManager.PickUpEffect();
                Destroy(obj.gameObject);
            }
            
            
        }

        private void SpawnSymbol(Vector2 position, float degreeAngle) {
            _textGenerator.SpawnNextSymbol(position, degreeAngle);
        }

        private void SetViewHeroState(bool state) {
            _hero.gameObject.SetActive(state);
        }

        private IEnumerator StartLevelCoroutine(Action startScore) {
            yield return new WaitForSeconds(1);
            _hero.SetMoveState(true);
            _mover.SetMoveState(true);
            _textGenerator.StartLevel(_heroSettings.Speed);
             startScore?.Invoke();
        }

        private void SpawnHero() {
            if(_hero != null) return;
            _hero = Instantiate(_heroSettings.heroPrefab, _startPosition, quaternion.identity);
            _hero.gameObject.SetActive(false);
        }

        public void SubscribeOnLevelEnd(Action callback) {
            _onLevelEnd += callback;
        }
    }
}
