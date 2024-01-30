using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class GameUI : MonoBehaviour {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Image _background;

        public void SetScore(int score) {
            _score.text = score.ToString();
        }

        public void SetBackground(Sprite background) {
            _background.sprite = background;
        }
    }
}
