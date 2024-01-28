using System;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TextGenerator {
    public class TextGenerator : MonoBehaviour {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _symbolPrefab;
        [SerializeField] private List<string> _jokes;
        
        private Pool _symbolsPool;
        private string _currentJoke;
        private int _currentSymbol;
        private List<GameObject> _activeSymbols;
        private float _currentSpeed;
        private float _canvasHalfWidth;
        private float _canvasHalfWidthWorld;
        private bool _islevelStart;

        public void Init() {
            _symbolsPool = new Pool(_symbolPrefab.gameObject, 10, _canvas.transform);
            _activeSymbols = new List<GameObject>();
            _canvasHalfWidth = Screen.width / 2f;
            _canvasHalfWidthWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0 ,0 )).x;
        }

        public void StartLevel(float currentSpeed) {
            _currentJoke = _jokes[Random.Range(0, _jokes.Count)];
            _currentSymbol = 0;
            _currentSpeed = currentSpeed;
            _islevelStart = true;
        }

        public void EndLevel() {
            foreach (var activeSymbol in _activeSymbols) {
                _symbolsPool.Release(activeSymbol);
            }
            _activeSymbols.Clear();
            _islevelStart = false;
        }

        public void SpawnNextSymbol(Vector2 worldPosition, float degreeAngle)   {
            var screePosition = Camera.main.WorldToScreenPoint(worldPosition);
            var symbol = _symbolsPool.Take();
            RotateSymbol(symbol.transform, degreeAngle);
            
            symbol.GetComponent<TMP_Text>().text = _currentJoke[_currentSymbol].ToString();
            //symbol.transform.position = screePosition;
            symbol.transform.position = worldPosition;
            symbol.SetActive(true);
            
            _activeSymbols.Add(symbol);

            _currentSymbol++;
            if (_currentSymbol >= _currentJoke.Length)
                _currentSymbol = 0;
        }

        private void Update() {
            if(!_islevelStart) return;
            if(_activeSymbols == null || _activeSymbols.Count == 0) return;

            Vector3 deltaPosition = new Vector3(Time.deltaTime * _currentSpeed , 0, 0);
            var symbolsToRelease = new List<GameObject>();
            foreach (var symbol in _activeSymbols) {
                symbol.transform.position -= deltaPosition;
                
                if (MathF.Abs(symbol.transform.position.x) > _canvasHalfWidthWorld) {
                    symbolsToRelease.Add(symbol);
                    _symbolsPool.Release(symbol);
                }
            }

            foreach (var symbol in symbolsToRelease) {
                _activeSymbols.Remove(symbol);
            }
        }
        
        private void RotateSymbol(Transform symbolTransform, float degreeAngle) {
            symbolTransform.rotation = Quaternion.AngleAxis(degreeAngle, Vector3.forward);
        }
    }
}