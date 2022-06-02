using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace ShakaCat {
	public class AnimationUIPanel : MonoBehaviour {
		[Header("오브젝트")]
		public RectTransform Panel;

		public Mask Mask;

		[Header("설정")]
		public float AnimationTime = 0.5f;

		public bool UseAnchorMinMax;

		[HideIf("UseAnchorMinMax")]
		public Vector2 ShowPosition;

		[HideIf("UseAnchorMinMax")]
		public Vector2 HidePosition;

		[ShowIf("UseAnchorMinMax")]
		public Vector2 ShowAnchorMin;

		[ShowIf("UseAnchorMinMax")]
		public Vector2 ShowAnchorMax = Vector2.one;

		[ShowIf("UseAnchorMinMax")]
		public Vector2 HideAnchorMin;

		[ShowIf("UseAnchorMinMax")]
		public Vector2 HideAnchorMax;

		[HideInInspector]
		public bool IsShowing;

		private static List<AnimationUIPanel> panelStack = new();

#if UNITY_2019_3_OR_NEWER && UNITY_EDITOR
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void InitStaticVariables() {
			panelStack = new List<AnimationUIPanel>();
		}
#endif

		protected static AnimationUIPanel topPanel => panelStack.Count > 0 ? panelStack[^1] : null;

		private void Start() {
			if (UseAnchorMinMax) {
				Panel.anchorMin = HideAnchorMin;
				Panel.anchorMax = HideAnchorMax;
				Panel.anchoredPosition = Vector2.zero;
			} else {
				Panel.anchoredPosition = HidePosition;
			}

			if (!Mask.SafeIsUnityNull())
				Mask.enabled = true;
		}

		private void OnDisable() {
			Panel.DOKill();
		}

		[ButtonGroup]
		public void Show() {
			Panel.DOKill();
			if (UseAnchorMinMax) {
				Panel.DOAnchorMin(ShowAnchorMin, AnimationTime);
				Panel.DOAnchorMax(ShowAnchorMax, AnimationTime);
			} else {
				Panel.DOAnchorPos(ShowPosition, AnimationTime);
			}

			IsShowing = true;
			PushToPanelStack();
		}

		[ButtonGroup]
		public void Hide() {
			Panel.DOKill();
			if (UseAnchorMinMax) {
				Panel.DOAnchorMin(HideAnchorMin, AnimationTime);
				Panel.DOAnchorMax(HideAnchorMax, AnimationTime);
			} else {
				Panel.DOAnchorPos(HidePosition, AnimationTime);
			}
			IsShowing = false;
			PopFromPanelStack();
		}

		[ButtonGroup]
		public void Toggle() {
			if (IsShowing) Hide();
			else Show();
		}

		protected void PushToPanelStack() {
			if (panelStack.Contains(this)) panelStack.Remove(this);
			panelStack.Add(this);
		}

		protected void PopFromPanelStack() {
			panelStack.Remove(this);
		}
	}
}
