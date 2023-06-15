using System;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	[RequireComponent(typeof(Collider))]
	public class InteractableButton : MonoBehaviour, IInteractable
	{
		public static event Action<Answer> OnPlayerGiveAnswer;
		
		[SerializeField] private Answer _buttonAnswer;

		public void OnButtonSelect()
		{
			OnPlayerGiveAnswer?.Invoke(_buttonAnswer);
		}
	}
}
