using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Managers
{
	public class TimeManager : SingletonBehaviour<TimeManager>
	{
		public static event Action OnTimeEnd; 
		
		[SerializeField] private Image _timerImage;

		private float _time = 0f;
		private bool _isTimerOn = false;

		private float _decreamentDiv;

		private void OnEnable()
		{
            
		}

		private void OnDisable()
		{
            
		}

		private void Update()
		{
			if (Input.GetKey(KeyCode.I))
			{
				SetTimerAndReStart(10f);
			}
			
			if(!_isTimerOn) return;

			_time -= Time.deltaTime;
			UpdateTimer(_time);
		}

		public void SetTimerAndReStart(float targetDuration)
		{
			_time = targetDuration;
			_decreamentDiv = targetDuration;
			_timerImage.fillAmount = 1f;
			PlayTimer();
		}
		
		public void PlayTimer()
		{
			_isTimerOn = true;
		}

		public void PauseTimer()
		{
			_isTimerOn = false;
		}
		
		public void ToggleTimer()
		{
			_isTimerOn = !_isTimerOn;
		}
		
		private void UpdateTimer(float currentTime)
		{
			_timerImage.fillAmount = currentTime / _decreamentDiv;
			if (_timerImage.fillAmount <= 0)
			{
				_isTimerOn = false;
				
				_time = 0f;
				_timerImage.fillAmount = 0f; 
				
				Debug.Log("Sure biter");
				OnTimeEnd?.Invoke();
			}
		}

		protected override void OnAwake() { }
	}
}
