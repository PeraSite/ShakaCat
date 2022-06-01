using DG.Tweening;
using UnityEngine;

namespace ShakaCat {
	public class DrinkMakeUI : MonoBehaviour {
		[Header("오브젝트")]
		public RectTransform DrinkMakePanel;

		[Header("설정")]
		public float HideX;

		public float AnimationTime;

		public void HideDrinkMakeUI() {
			DrinkMakePanel.DOAnchorPosX(HideX, AnimationTime);
		}
	}
}
