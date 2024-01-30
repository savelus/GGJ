using System;
using Levels;
using TMPro;
using UnityEngine;

namespace UI.Menu {
    [Serializable]
    public class LabelWithLevelType {
        public LevelType LevelType;
        public TMP_Text Label;
    }
}