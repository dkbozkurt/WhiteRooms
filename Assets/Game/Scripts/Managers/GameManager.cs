using System;
using Game.Scripts.Behaviours;
using Game.Scripts.Helpers;
using Game.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Managers
{
	public class GameManager : SingletonBehaviour<GameManager>
	{
		public static event Action OnGameStart;
		public static event Action OnTutorialEnd;
		
		public GameDataScriptableObject DATA;

		[SerializeField] private TextMeshProUGUI _questionText;

		[HideInInspector] public bool CanMove = false;
		[HideInInspector] public bool CanLookAround = false;
		[HideInInspector] public bool CanInteract = false;
		
		private int _sectionNumber = 0;
		
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

		private void SetQuestionText(string str)
		{
			_questionText.text = str;
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

			if (Input.GetKeyDown(KeyCode.T))
			{
				SetQuestionText(DATA.GetSectionQuestion(-1));
			}
			
			if (Input.GetKeyDown(KeyCode.Y))
			{
				SetQuestionText(DATA.GetSectionQuestion(0));
			}
		}
	}
}
