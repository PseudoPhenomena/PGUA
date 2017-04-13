using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
    [RequireComponent(typeof(Image))]
    public class BarViewSizeImageFill : ProgressBarProView {
        
        [SerializeField] protected Image image;
		[SerializeField] bool hideOnEmpty = true;
        [SerializeField] bool useDiscreteSteps = false;
        [SerializeField] int numSteps = 10;

        public override void UpdateView(float currentValue, float targetValue) {
			if (hideOnEmpty && currentValue <= 0f) {
                image.gameObject.SetActive(false);
                return;
            }

            image.gameObject.SetActive(true);
			image.fillAmount = GetDisplayValue(currentValue);
        }

        float GetDisplayValue(float display) {
            if (!useDiscreteSteps)
                return display;

            return Mathf.Round(display * numSteps) / numSteps;
        }

#if UNITY_EDITOR
		protected override void Reset() {
			base.Reset();
            image = GetComponent<Image>();
        }
#endif
    }

}