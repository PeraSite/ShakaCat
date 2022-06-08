using DG.Tweening;
using PeraCore.Runtime;
using TMPro;
using UnityEngine;

namespace ShakaCat {
	public class ToastSystem : MonoSingleton<ToastSystem> {
		protected override bool KeepAlive => false;

		[Header("오브젝트")]
		public RectTransform ToastObject;

		public TextMeshProUGUI Text;

		[Header("설정")]
		public float ShowY;

		public float HideY;

		public float AnimationTime = 0.5f;

		public float ShowingTime = 1f;

		public void ShowToast(string message) {
			ToastObject.DOKill(true);
			Text.text = message;
			DOTween.Sequence(ToastObject)
				.Append(ToastObject.DOPivotY(ShowY, AnimationTime))
				.AppendInterval(ShowingTime)
				.Append(ToastObject.DOPivotY(HideY, AnimationTime));
		}

		private void OnDisable() {
			ToastObject.DOKill();
		}
	}
}
