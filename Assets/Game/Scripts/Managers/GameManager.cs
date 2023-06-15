using System;
using Game.Scripts.Behaviours;
using Game.Scripts.Helpers;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Managers
{
	public class GameManager : SingletonBehaviour<GameManager>
	{
		public static event Action OnGameStart;
		public static event Action OnTutorialEnd;
		
		public GameDataScriptableObject DATA;

		public bool CanMove = false;
		public bool CanLookAround = false;
		public bool CanInteract = false;
		
		private int _sectionNumber = 0;
		
		private void Awake()
		{
        
		}

		protected override void OnAwake() { }

		private void OnEnable()
		{
			InteractableButton.OnPlayerGiveAnswer += CheckAnswer;
		}
	
		private void OnDisable()
		{
			InteractableButton.OnPlayerGiveAnswer -= CheckAnswer;
		}	
	
		private void Start()
		{
			
		}

		private void CheckAnswer(Answer givenAnswer)
		{
			Debug.Log("Given answer is " + givenAnswer);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				OnGameStart?.Invoke();
				CanMove = true;
				CanLookAround = true;
				CanInteract = true;
				Debug.Log(DATA.GetSectionQuestion(0));
			}
		}
	}
}
