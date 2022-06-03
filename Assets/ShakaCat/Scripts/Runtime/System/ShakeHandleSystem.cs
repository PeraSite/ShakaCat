using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShakeHandleSystem : MonoBehaviour {
	[Header("오브젝트")]
	public IntVariable ShakeCounter;

	[Header("설정")]
	public float ShakeTolerance;

	public float MinShakeInterval;
	public bool DetectingShake;

	private float _timeSinceLastShake;
	private bool _hasAccelerometer;

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
		ResetData();
		DetectingShake = true;
	}

	public void ResetData() {
		ShakeCounter.Value = 0;
		DetectingShake = false;
	}

	private void Update() {
		if (!_hasAccelerometer) return;
		if (!DetectingShake) return;

		var accel = Accelerometer.current.acceleration.ReadValue();
		if (accel.magnitude > ShakeTolerance) {
			if (Time.unscaledTime >= _timeSinceLastShake + MinShakeInterval) {
				ShakeCounter.Add(1);
				_timeSinceLastShake = Time.unscaledTime;
			}
		}
	}

	[Button]
	public void SimulateShake() {
		ShakeCounter.Add(1);
	}
}
