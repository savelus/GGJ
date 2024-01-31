using System;
using System.Collections.Generic;
using System.Linq;
using Levels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private List<LabelWithLevelType> _records;

        public void SubscribeOnStartButton(UnityAction callback) {
            _startButton.onClick.AddListener(callback);
        }

        public void SetRecord(LevelType levelType, int record) {
            var textBlock = _records.FirstOrDefault(x => x.LevelType == levelType);
            if(textBlock==null)return;
            
            textBlock.Label.text = record.ToString();
        }

        public void ViewAllRecords() {
            foreach (var recordWithType in _records) {
                recordWithType.Label.text = PlayerPrefs.GetInt(recordWithType.LevelType.ToString()).ToString();
            }
        }
    }
}
