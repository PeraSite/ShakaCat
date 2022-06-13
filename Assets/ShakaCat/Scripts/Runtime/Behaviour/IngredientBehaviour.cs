using System;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShakaCat {
	public class IngredientBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
		[Header("오브젝트")]
		public TextMeshProUGUI Name;

		public GameObject UnlockContainer;

		public TextMeshProUGUI UnlockPrice;

		[Header("변수")]
		public IngredientDataValueList CurrentIngredient;

		public IngredientDataValueList UnlockedIngredients;

		public IngredientData Data;

		public IntVariable Money;

		[Header("사운드")]
		public SoundEffectSO UnlockSound;
		public SoundEffectSO DropSound;

		[Header("설정")]
		public float DragObjectAlpha = 0.5f;

		private GameObject _instantiated;
		private RectTransform _rect;

		private bool IsUnlocked => UnlockedIngredients.Contains(Data);

		private void Awake() {
			UnlockedIngredients.Added.Register(UpdateUnlockContainer);
			UnlockedIngredients.Removed.Register(UpdateUnlockContainer);

			UnlockPrice.text = Data.UnlockPrice + "원";
		}

		private void OnDisable() {
			UnlockedIngredients.Added.Unregister(UpdateUnlockContainer);
			UnlockedIngredients.Removed.Unregister(UpdateUnlockContainer);
		}

		private void UpdateUnlockContainer(IngredientData target) {
			UnlockContainer.SetActive(!IsUnlocked);
		}

		[Button]
		public void Setup() {
#if UNITY_EDITOR
			if (Data.SafeIsUnityNull()) throw new Exception("Ingredient Data is null!");
			var image = GetComponent<Image>();
			image.sprite = Data.BottleImage;
			Name.text = Data.Name;
			gameObject.name = Data.name;
			UnityEditor.EditorUtility.SetDirty(Name);
			UnityEditor.EditorUtility.SetDirty(image);
#endif
		}

		public void OnBeginDrag(PointerEventData eventData) {
			if (!IsUnlocked) return;

			if (Money.Value < Data.Price) {
				ToastSystem.Instance.ShowToast("재료비가 부족합니다!");
				return;
			}

			Money.Subtract(Data.Price);

			_instantiated = Instantiate(gameObject, transform.parent);
			_rect = _instantiated.GetComponent<RectTransform>();

			var image = _instantiated.GetComponent<Image>();
			image.color = new Color(image.color.r, image.color.g, image.color.b, DragObjectAlpha);
			image.maskable = false;
			image.raycastTarget = false;
			_instantiated.GetComponentInChildren<TextMeshProUGUI>().maskable = false;
		}

		public void OnDrag(PointerEventData eventData) {
			if (!IsUnlocked) return;
			if (_instantiated.SafeIsUnityNull()) return;
			_rect.position = eventData.position;
		}

		public void OnEndDrag(PointerEventData eventData) {
			if (!IsUnlocked) return;
			if (_instantiated.SafeIsUnityNull()) return;

			Destroy(_instantiated);
			_instantiated = null;

			var hovered = eventData.hovered;
			var isShaker = hovered.Any(go => go.TryGetComponent<ShakerBehaviour>(out var shaker));
			if (isShaker) {
				DropSound.Play();
				CurrentIngredient.Add(Data);
			} else {
				Money.Add(Data.Price);
			}
		}

		public void Unlock() {
			if (Money.Value < Data.UnlockPrice) {
				ToastSystem.Instance.ShowToast("해금 비용이 부족합니다!");
				return;
			}

			Money.Subtract(Data.UnlockPrice);
			UnlockedIngredients.Add(Data);
			UnlockSound.Play();
		}
	}
}
