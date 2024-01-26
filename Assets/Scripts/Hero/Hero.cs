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


        public void Init(HeroSettings heroSettings, float fieldHalfHeight) {
            _degreeAngle = heroSettings.Angle;
            _radianAngle = heroSettings.Angle * Mathf.Deg2Rad;
            _speed = heroSettings.Speed;
            _fieldHalfHeight = fieldHalfHeight;

            _isMove = false;

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
            if (_isMove) Move();
        }

        private void Move() {
            var deltaY = _speed * Time.deltaTime * Mathf.Sin(_radianAngle);

            if (Mathf.Abs(deltaY + transform.position.y) >= _fieldHalfHeight) return;

            transform.position += new Vector3(0, deltaY, 0);
        }
    }
}
