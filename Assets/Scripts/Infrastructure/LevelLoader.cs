using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Input;
using Levels;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;

namespace Infrastructure {
    public class LevelLoader : MonoBehaviour {
        public Action OnLevelEnd;
        
        private Vector2 _startPosition;
        private HeroSettings _heroSettings;
        private ComputerInputSystem _inputSystem;
        private LevelMover _mover;

        private Hero.Hero _hero;

        public void Init(HeroSettings heroSettings, 
                         Vector2 startPosition, 
                         ComputerInputSystem inputSystem, 
                         LevelMover mover) {
            _mover = mover;
            _heroSettings = heroSettings;
            SpawnHero();
             _startPosition = startPosition;

            _inputSystem = inputSystem;
            _inputSystem.SubscribeOnChangeDirection(_hero.ChangeDirection);

        }
        
        public void RunLevel(GameObject currentLevel) {
            ViewHero();
            _mover.Init(_heroSettings.Speed, currentLevel);
            StartCoroutine(StartLevelCoroutine());
        }

        public void EndLevel() {
            _hero.SetMoveState(false);
            SetViewHeroState(false);
            
            OnLevelEnd?.Invoke();
        }

        private void ViewHero() {
            if (_hero == null) {
                print("Hero not instantiate");
                return;
            }

            _hero.Init(_heroSettings, Camera.main.orthographicSize);
            _hero.transform.position = _startPosition;
            SetViewHeroState(true);
        }

        private void SetViewHeroState(bool state) {
            _hero.gameObject.SetActive(state);
        }

        private IEnumerator StartLevelCoroutine() {
            yield return new WaitForSeconds(3);
            _hero.SetMoveState(true);
            _mover.SetMoveState(true);
        }

        private void SpawnHero() {
            if(_hero != null) return;
            _hero = Instantiate(_heroSettings.heroPrefab, _startPosition, quaternion.identity);
            _hero.gameObject.SetActive(false);
        }
    }
}
