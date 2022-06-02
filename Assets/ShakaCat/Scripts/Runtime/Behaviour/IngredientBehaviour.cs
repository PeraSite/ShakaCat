using System;
using Lean.Touch;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShakaCat {
	public class IngredientBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
		[Header("오브젝트")]
		[OnValueChanged("Setup")]
		public IngredientData Data;
		public TextMeshProUGUI Name;

		public IngredientDataValueList CurrentIngredient;

		[Header("설정")]
		public float DragObjectAlpha = 0.5f;

		private GameObject _instantiated;
		private RectTransform _rect;

		[Button]
		public void Setup() {
			if (Data.SafeIsUnityNull()) throw new Exception("Ingredient Data is null!");
			GetComponent<Image>().sprite = Data.BottleImage;
			Name.text = Data.Name;
			gameObject.name = Data.name;
		}

		public void OnBeginDrag(PointerEventData eventData) {
			_instantiated = Instantiate(gameObject, transform);
			_rect = _instantiated.GetComponent<RectTransform>();

			var image = _instantiated.GetComponent<Image>();
			image.color = new Color(image.color.r, image.color.g, image.color.b, DragObjectAlpha);
			image.raycastTarget = false;
		}

		public void OnDrag(PointerEventData eventData) {
			_rect.position = eventData.position;
		}

		public void OnEndDrag(PointerEventData eventData) {
			Destroy(_instantiated);
			_instantiated = null;

			var hovered = eventData.hovered;
			if (hovered.Find(go => go.TryGetComponent<ShakerBehaviour>(out var shaker))) {
				CurrentIngredient.Add(Data);
			}
		}
	}
}
