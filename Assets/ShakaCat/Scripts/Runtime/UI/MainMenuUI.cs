using PixelCrushers;
using UnityEngine;

namespace ShakaCat {
	public class MainMenuUI : MonoBehaviour {
		public GameObject NewGameButton;
		public GameObject ContinueButton;

		public int SaveGameSlot = 1;

		private void Start() {
			var hasSave = SaveSystem.HasSavedGameInSlot(SaveGameSlot);

			NewGameButton.SetActive(!hasSave);
			ContinueButton.SetActive(hasSave);
		}

		public void StartGame() {
			GameModeManager.Instance.HandleStartRequested(GameModeManager.Instance.playMode);
		}

		public void OpenSettingPanel() { }

		public void CloseSettingPanel() { }

		public void Quit() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
	Application.Quit();
#endif
		}
	}
}
