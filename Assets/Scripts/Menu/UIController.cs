using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _textRecord;

        public void SubscribeOnStartButton(UnityAction callback) {
            _startButton.onClick.AddListener(callback);
        }

        public void SetRecord(int record) {
            _textRecord.text = record.ToString();
        }
    }
}
