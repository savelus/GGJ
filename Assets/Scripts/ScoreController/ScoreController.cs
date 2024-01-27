using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace ScoreController {
    public class ScoreController : MonoBehaviour {
        private Action<int> _scoreChanged;

        private float timer;

        private bool _scoreState;
        
        public void StartScore() {
            _scoreState = true;
            timer = 0;
            _scoreChanged?.Invoke(0);
        }

        public void EndScore() {
            _scoreState = false;
        }

        public void SubscribeOnScoreChanged(Action<int> callback) {
            _scoreChanged += callback;
        }

        public void AddScore(float score) {
            timer += score;
            _scoreChanged?.Invoke((int)timer);
        }

        private void Update() {
            if(!_scoreState) return;
            if ((int)(timer + Time.deltaTime) > timer) {
                _scoreChanged?.Invoke((int)(timer + Time.deltaTime));
            }
            timer += Time.deltaTime;
        }
    }
}