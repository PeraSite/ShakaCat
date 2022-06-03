using System;
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


		[Header("설정")]
		public float DragObjectAlpha = 0.5f;

		private GameObject _instantiated;
		private RectTransform _rect;

		private bool IsUnlocked => UnlockedIngredients.Contains(Data);

		private void Awake() {
			UnlockedIngredients.Added.Register(UpdateUnlockContainer);
			UnlockedIngredients.Removed.Register(UpdateUnlockContainer);

			UnlockPrice.text = Data.UnlockPrice + "원";
			// UnlockedIngredients.Cleared.Register(UpdateUnlockContainer);
		}

		private void OnDisable() {
			UnlockedIngredients.Added.Unregister(UpdateUnlockContainer);
			UnlockedIngredients.Removed.Unregister(UpdateUnlockContainer);
			// UnlockedIngredients.Cleared.Unregister(UpdateUnlockContainer);
		}

		private void UpdateUnlockContainer(IngredientData target) {
			UnlockContainer.SetActive(!IsUnlocked);
		}

		[Button]
		public void Setup() {
#if UNITY_EDITOR
			if (Data.SafeIsUnityNull()) throw new Exception("Ingredient Data is null!");
			GetComponent<Image>().sprite = Data.BottleImage;
			Name.text = Data.Name;
			gameObject.name = Data.name;
			UnityEditor.EditorUtility.SetDirty(Name);
#endif
		}

		public void OnBeginDrag(PointerEventData eventData) {
			if (!IsUnlocked) return;

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
			_rect.position = eventData.position;
		}

		public void OnEndDrag(PointerEventData eventData) {
			if (!IsUnlocked) return;
			Destroy(_instantiated);
			_instantiated = null;

			var hovered = eventData.hovered;
			if (hovered.Find(go => go.TryGetComponent<ShakerBehaviour>(out var shaker))) {
				CurrentIngredient.Add(Data);
			}
		}

		public void Unlock() {
			if (Money.Value > Data.UnlockPrice) {
				Money.Subtract(Data.UnlockPrice);
				UnlockedIngredients.Add(Data);
			}
		}
	}
}
