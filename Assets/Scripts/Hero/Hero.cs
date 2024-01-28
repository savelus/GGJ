using System;
using ScriptableObjects;
using UnityEngine;

namespace Hero {
    public class Hero : MonoBehaviour {
        [SerializeField] private SpriteRenderer _heroSprite;

        private int _degreeAngle;
        private float _radianAngle;
        private int _speed;

        private float _fieldHalfHeight;
        private bool _isMove;
        private Action<Collider2D> _onTriggerAction;
        private Action<Vector2, float> _onSpawnSymbol;
        private float _timer;

        public void Init(HeroSettings heroSettings, 
                         float fieldHalfHeight, 
                         Action<Collider2D> onTriggerAction, 
                         Action<Vector2, float> onSpawnSymbol,
                         Sprite heroSprite) {
            _degreeAngle = heroSettings.Angle;
            _radianAngle = heroSettings.Angle * Mathf.Deg2Rad;
            _speed = heroSettings.Speed;
            _fieldHalfHeight = fieldHalfHeight;
            _heroSprite.sprite = heroSprite;
            _isMove = false;

            _onTriggerAction = onTriggerAction;
            _onSpawnSymbol = onSpawnSymbol;
            
            RotateSprite();
        }

        public void SetMoveState(bool state) {
            _isMove = state;
        }
        
        public void ChangeDirection() {
            _radianAngle *= -1;
            _degreeAngle *= -1;
            
            RotateSprite();
        }

        private void RotateSprite() {
            _heroSprite.transform.rotation = Quaternion.AngleAxis(_degreeAngle, Vector3.forward);
        }

        private void Update() {
            if (!_isMove) return;
            
            Move();
            _timer += Time.deltaTime * _speed;

            if (_timer > 0.55) {
                _onSpawnSymbol?.Invoke(transform.position, _degreeAngle);
                _timer = 0;
            }
        }

        private void Move() {
            var deltaY = _speed * Time.deltaTime * Mathf.Sin(_radianAngle);

            if (Mathf.Abs(deltaY + transform.position.y) >= _fieldHalfHeight) return;

            transform.position += new Vector3(0, deltaY, 0);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            _onTriggerAction?.Invoke(other);
        }
    }
}
