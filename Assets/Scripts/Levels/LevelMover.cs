using UnityEngine;

namespace Levels {
    public class LevelMover : MonoBehaviour {
        private GameObject _level;
        private float _speed;
        private bool _moveState;

        public void Init(float speed, GameObject level) {
            gameObject.SetActive(true);
            _speed = speed;
            _level = Instantiate(level, transform);
        }

        public void SetMoveState(bool state) {
            _moveState = state;
        }

        private void Update() {
            if(!_moveState) return;
            MoveLevel();
        }

        private void MoveLevel() {
            _level.transform.position -= new Vector3(_speed * Time.deltaTime, 0);
        }

        public void EndLevel() {
            SetMoveState(false);
            gameObject.SetActive(false);
            Destroy(_level);
        }
    }
}