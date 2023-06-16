using System;
using DG.Tweening;
using Game.Scripts.Helpers;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	[RequireComponent(typeof(Collider))]
	public class InteractableButton : MonoBehaviour, IInteractable
	{
		public static event Action<Answer> OnPlayerGiveAnswer;
		
		[SerializeField] private Answer _buttonAnswer;
		[SerializeField] private Vector3 _initialPosition = new Vector3(0.5f, 0.572f, 4.329f);
		[SerializeField] private Vector3 _pressedPosition = new Vector3(0.5f,0.489f,4.412f);

		private void Start()
		{
			transform.localPosition = _initialPosition;
		}

		public void OnButtonSelect()
		{
			AudioManager.Instance.PlaySound(AudioName.ButtonPress);
			ButtonPressAnimation();
			OnPlayerGiveAnswer?.Invoke(_buttonAnswer);
		}

		private void ButtonPressAnimation()
		{
			transform.localPosition = _initialPosition;
			transform.DOLocalMove(_pressedPosition,0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
			{
				transform.DOLocalMove(_initialPosition, 0.5f).SetEase(Ease.Linear);
			});
		}
	}
}
