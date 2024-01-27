using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _endButton;
        [SerializeField] private TextMeshProUGUI _textRecord;

        void Start()
        {
            _startButton.onClick.AddListener(StartOnClick);
            _endButton.onClick.AddListener(EndOnClick);
            SetRecord(0);
        }

        private void StartOnClick()
        {
            Debug.Log("You have clicked start");
        }

        private void EndOnClick()
        {
            Debug.Log("You have clicked end");
        }

        public void SetRecord(int record)
        {
            _textRecord.text = record.ToString();
        }
    }
}
