using System;
using UnityEngine;

namespace Infrastructure.Input {
    public class ComputerInputSystem : MonoBehaviour {
        private Action _directionChanged;

        private const KeyCode DirectionButton = KeyCode.Space;
        
        public void SubscribeOnChangeDirection(Action callback) {
            _directionChanged += callback;
        }

        private void Update() {
            if (UnityEngine.Input.GetKeyDown(DirectionButton)) {
                _directionChanged?.Invoke();
            }
        }
    }
}