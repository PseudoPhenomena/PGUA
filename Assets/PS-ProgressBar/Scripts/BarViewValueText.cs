using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
	[RequireComponent(typeof(Text))]
	public class BarViewValueText : ProgressBarProView {

		[SerializeField] Text text;
		[SerializeField] string prefix = "";
		[SerializeField] float minValue = 0f;
		[SerializeField] float maxValue = 100f;
		[SerializeField] int numDecimals = 0;
		[SerializeField] bool showMaxValue = false;
		[SerializeField] string numberUnit = "%";
		[SerializeField] string suffix = "";

		public override void UpdateView(float currentValue, float targetValue) {
			text.text = prefix + GetDisplayNumber(currentValue) + (showMaxValue ? " / " + FormatNumber(maxValue) : "" ) + suffix;
		}

		string GetDisplayNumber(float num) {
			return FormatNumber(Mathf.Lerp(minValue, maxValue, num));
		}

		string FormatNumber(float num){
			return num.ToString("N"+numDecimals)+numberUnit;
		}

		#if UNITY_EDITOR
		protected override void Reset() {
			base.Reset();
			text = GetComponent<Text>();
		}
		#endif
	}

}