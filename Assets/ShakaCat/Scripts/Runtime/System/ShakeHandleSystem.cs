using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShakeHandleSystem : MonoBehaviour {
	public float ShakeTolerance;
	public float MinShakeInterval;

	public bool DetectingShake;

	public TextMeshProUGUI ShakeTime;
	public TextMeshProUGUI ShakeCounter;

	private float _shakeTime;
	private int _shakeCounter;
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
		ShakeTime.text = "0.0초";
		ShakeCounter.text = "0회";
		_shakeTime = 0f;
		DetectingShake = false;
	}

	private void Update() {
		if (!_hasAccelerometer) return;
		if (!DetectingShake) return;

		var accel = Accelerometer.current.acceleration.ReadValue();
		if (accel.magnitude > ShakeTolerance) {
			_shakeTime += Time.deltaTime;
			if (Time.unscaledTime >= _timeSinceLastShake + MinShakeInterval) {
				_shakeCounter++;
				_timeSinceLastShake = Time.unscaledTime;
			}
			UpdateUI();
		}
	}

	[Button]
	public void SimulateShake() {
		_shakeTime += 1f;
		_shakeCounter++;
		UpdateUI();
	}

	private void UpdateUI() {
		ShakeTime.text = _shakeTime.ToString("F1") + "초";
		ShakeCounter.text = _shakeCounter + "회";
	}
}
