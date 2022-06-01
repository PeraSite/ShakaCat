using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AccelerationProvider : MonoBehaviour {
	public float ShakeTolerance = 5f;
	public float MinShakeInterval;

	public TextMeshProUGUI Accel;
	public TextMeshProUGUI Shake;

	private int _shakeAmount;
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

	private void Update() {
		if (!_hasAccelerometer) return;
		var accel = Accelerometer.current.acceleration.ReadValue();


		DetectShake(accel);
		Accel.text = accel.ToString();
		Shake.text = _shakeAmount.ToString();
	}

	private void DetectShake(Vector3 accel) {
		if (accel.magnitude > ShakeTolerance && Time.unscaledTime >= _timeSinceLastShake + MinShakeInterval) {
			_shakeAmount++;
			_timeSinceLastShake = Time.unscaledTime;
		}
	}
}
