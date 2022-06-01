using Lean.Touch;
using UnityAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShakaCat {
	public class IngredientBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
		[Header("설정")]
		public GameObject Prefab;

		public IngredientData Data;

		public IngredientDataValueList CurrentIngredient;

		private GameObject _instantiated;
		private RectTransform _rect;

		public void OnBeginDrag(PointerEventData eventData) {
			_instantiated = Instantiate(Prefab, transform);
			_rect = _instantiated.GetComponent<RectTransform>();
			_instantiated.GetComponent<Image>().raycastTarget = false;
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
