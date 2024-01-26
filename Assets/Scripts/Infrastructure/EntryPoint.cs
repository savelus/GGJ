using System;
using System.Collections;
using Infrastructure.Input;
using ScriptableObjects;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastructure {
    public class EntryPoint : MonoBehaviour {
        [SerializeField] private HeroSettings _heroSettings;
        [SerializeField] private ComputerInputSystem _inputSystem;
        
        [SerializeField] private Vector2 _startPosition;

        private Hero.Hero _hero;

        private void Start() {
            SpawnHero();
            _inputSystem.SubscribeOnChangeDirection(_hero.ChangeDirection);
            StartCoroutine(RunGame());
        }

        private IEnumerator RunGame() {
            yield return new WaitForSeconds(3);
            StartMoveHero();
        }

        private void SpawnHero() {
            _hero = Instantiate(_heroSettings.heroPrefab, _startPosition, quaternion.identity);
            _hero.gameObject.SetActive(true);
            _hero.Init(_heroSettings, Camera.main.orthographicSize);
        }

        private void StartMoveHero() {
            _hero.SetMoveState(true);
        }
    }
}