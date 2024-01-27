using System.Collections.Generic;
using UnityEngine;

namespace Tools {
    public class Pool {
        private Queue<GameObject> _pool;
        
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public Pool(GameObject prefab, int initialSize, Transform parent) {
            _prefab = prefab;
            _parent = parent;
            
            _pool = new Queue<GameObject>(initialSize);
            
            for (int i = 0; i < initialSize; i++) {
                CreateNewObject();
            }
        }

        private void CreateNewObject() {
            var gameObject = Object.Instantiate(_prefab, _parent);
            gameObject.SetActive(false);
            _pool.Enqueue(gameObject);
        }

        public GameObject Take() {
            if (_pool.Count == 0) CreateNewObject();
            return _pool.Dequeue();
        }

        public void Release(GameObject gameObject) {
            gameObject.SetActive(false);
            _pool.Enqueue(gameObject);
        }
    }
}