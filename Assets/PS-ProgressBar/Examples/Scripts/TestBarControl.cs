using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar { 
    public class TestBarControl : MonoBehaviour {

        [SerializeField] Transform barParent;
        ProgressBarPro[] bars;
        Button[] buttons;
        Slider slider;

	    void Start () {
			bars = barParent.GetComponentsInChildren<ProgressBarPro>(true);
            buttons = GetComponentsInChildren<Button>();
            slider = GetComponentInChildren<Slider>();
            SetupButtons();
	    }

        void SetupButtons() {
            Text text;
            Button button;

            for (int i = 0; i < buttons.Length; i++) {
                float currentValue = i / (float)(buttons.Length - 1);

                button = buttons[i];
                button.name = "Button_" + currentValue;
                text = button.GetComponentInChildren<Text>();
                text.text = currentValue.ToString();
                button.onClick.AddListener(delegate { SetSlider(currentValue); });
            }
        }

        void SetSlider(float value) {
            // This automatically controls the value of all bars
            slider.value = value;
        }

        public void SetBars(float value) {
            for (int i = 0; i < bars.Length; i++) 
                bars[i].SetValue(value);
        }
    }

}