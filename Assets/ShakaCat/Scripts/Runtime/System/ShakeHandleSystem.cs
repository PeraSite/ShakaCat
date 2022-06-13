using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShakeHandleSystem : MonoBehaviour {
	[Header("오브젝트")]
	public IntVariable ShakeCounter;

	[Header("사운드")]
	public SoundEffectSO ShakeSound;


	[Header("설정")]
	public float ShakeTolerance;

	public float MinShakeInterval;
	public bool DetectingShake;

	private float _timeSinceLastShake;
	private bool _hasAccelerometer;

	private AudioSource _shakingAudioSource;

	private void Awake() {
		_hasAccelerometer = Accelerometer.current != null;
	}

	private void OnEnable() {
		if (!_hasAccelerometer) return;
		InputSystem.EnableDevice(Accelerometer.current);
	}

	private void OnDisable() {
		if (!_hasAccelerometer) return;
		InputSystem.DisableDevice(Accelerometer.current);
	}

	public void StartShaking() {
		ShakeCounter.Value = 0;
		DetectingShake = true;
		_shakingAudioSource = ShakeSound.PlayAndReturn();
		_shakingAudioSource.loop = true;
	}

	public void EndShaking() {
		DetectingShake = false;
		if (!_shakingAudioSource.SafeIsUnityNull()) {
			_shakingAudioSource.Stop();
			_shakingAudioSource = null;
		}
	}

	private void Update() {
		if (!_hasAccelerometer) return;
		if (!DetectingShake) return;

		var accel = Accelerometer.current.acceleration.ReadValue();
		var shakeAmount = accel.magnitude;

		_shakingAudioSource.volume = shakeAmount <= 1 ? 0f : Mathf.Min(shakeAmount / ShakeTolerance, ShakeTolerance);

		if (shakeAmount > ShakeTolerance) {
			if (Time.unscaledTime >= _timeSinceLastShake + MinShakeInterval) {
				OnShake();
				_timeSinceLastShake = Time.unscaledTime;
			}
		}
	}

	private void OnShake() {
		ShakeCounter.Add(1);
	}

	[Button]
	public void SimulateShake() {
		OnShake();
	}
}
