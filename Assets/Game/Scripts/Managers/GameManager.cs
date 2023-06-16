using System;
using System.Collections.Generic;
using Game.Scripts.Behaviours;
using Game.Scripts.Helpers;
using Game.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Managers
{
	
	public class GameManager : SingletonBehaviour<GameManager>
	{
		public static event Action OnGameStart;
		public static event Action OnTutorialEnd;

		public static event Action OnColorResetCall;

		public GameDataScriptableObject DATA;

		[SerializeField] private TextMeshProUGUI _questionText;

		[HideInInspector] public bool CanMove = false;
		[HideInInspector] public bool CanLookAround = false;
		[HideInInspector] public bool CanInteract = false;

		[Header("Sections")] 
		[SerializeField] private SectionProps[] _sectionProps = new SectionProps[] { };
		
		private int _questionNumber = 0;
		
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
			SetColorsToDefault();
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
				StartTutorial();
				CanMove = true;
				CanLookAround = true;
				CanInteract = true;
			}

			if (Input.GetKeyDown(KeyCode.Y))
			{
				SetQuestionText(DATA.GetSectionQuestion(0));
			}

			if (Input.GetKeyDown(KeyCode.U))
			{
				AskQuestion();
			}
		}

		private void StartTutorial()
		{
			SetQuestionText(DATA.GetSectionQuestion(-1));
		}
		
		private void AskQuestion()
		{
			QuestionInfo question = DATA.GameQuestions[_questionNumber];
			SetQuestionText(DATA.GetSectionQuestion(_questionNumber));
			
			SetColorsForSection(question);
			
		}
		
		private void SetColorsForSection(QuestionInfo questionInfo)
		{
			// int numberOfCorrectObject = questionInfo.ActualNumber;
			// ObjectShape correctShape = questionInfo.ShapeToFind;
			// int randomColorSetObjectCount = questionInfo.RandomObjectCount;
			// Color targetCorrectColor = DATA.GetColorByColorInfo(questionInfo.TargetColor);
			//
			// int[] objectIndexArrayToSetCorrectColor = new int [questionInfo.ActualNumber];
			//
			// objectIndexArrayToSetCorrectColor =
			// 	GetRandomNumberArray(_sectionProps[0].ShapeAndBelonginsObjectsArray[0].ColoredObjects.Length, numberOfCorrectObject);
			//
			//
			// for (int i = 0; i < objectIndexArrayToSetCorrectColor.Length; i++)
			// {
			// 	Debug.Log(objectIndexArrayToSetCorrectColor[i]);
			// 	// _sectionProps[0].ShapeAndBelonginsObjectsArray[0].ColoredObjects[i].SetColor(targetCorrectColor);
			// }
			
		}

		private void CheckTheAnswer()
		{
			// TODO after clicking answer button hhere will be fired
		}

		private int[] GetRandomNumberArray(int arraySize, int targetRange)
		{
			HashSet<int> uniqueNumbers = new HashSet<int>();

			while (uniqueNumbers.Count < 3)
			{
				int randomNumber = Random.Range(0, targetRange);
				uniqueNumbers.Add(randomNumber);
			}

			int[] randomNumbersArray = new int[arraySize];
			uniqueNumbers.CopyTo(randomNumbersArray);

			return randomNumbersArray;
		}
		
		private void SetColorsToDefault()
		{
			OnColorResetCall?.Invoke();
		}
		
		
	}
}
