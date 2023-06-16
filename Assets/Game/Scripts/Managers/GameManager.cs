using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Behaviours;
using Game.Scripts.Helpers;
using Game.Scripts.ScriptableObjects;
using TMPro;
using UnityEditor.SearchService;
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
		[SerializeField] private ShapeAndBelonginsObjects[] shapeAndBelongins = new ShapeAndBelonginsObjects[] { };
		
		// Has to start from -1
		private int _questionNumber = -1;

		private Answer _expectedAnswerForTheSection = Answer.Yes;
		
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
			OnColorResetCall?.Invoke();
			
		}

		private void CheckAnswer(Answer givenAnswer)
		{
			TimeManager.Instance.PauseTimer();
			if (givenAnswer == _expectedAnswerForTheSection)
			{
				AudioManager.Instance.PlaySound(AudioName.Correct);
				OnColorResetCall?.Invoke();
				DOVirtual.DelayedCall(0.5f, () => NextQuestion());
				
			}
			else
			{
				AudioManager.Instance.PlaySound(AudioName.Fail);
				SceneManager.Instance.CallLoseScreen();
			}
		}
		
		public void StartGame()
		{
			OnGameStart?.Invoke();
			StartTutorial();
			CanMove = true;
			CanLookAround = true;
			CanInteract = true;
		}

		private void SetQuestionText(string str)
		{
			_questionText.text = str;
		}

		private void StartTutorial()
		{
			SetQuestionText(DATA.GetSectionQuestion(-1));
		}

		private void NextQuestion()
		{
			_questionNumber++;
			if (_questionNumber >= DATA.GameQuestions.Count)
			{
				SceneManager.Instance.CallWinScreen();
				return;
			}
			AskQuestion();
		}
		
		private void AskQuestion()
		{
			QuestionInfo question = DATA.GameQuestions[_questionNumber];
			TimeManager.Instance.SetTimerAndReStart(question.Duration);
			SetQuestionText(DATA.GetSectionQuestion(_questionNumber));
			
			SetPropsForQuestions(question);
		}
		
		private void SetPropsForQuestions(QuestionInfo questionInfo)
		{
			_expectedAnswerForTheSection = questionInfo.Answer;
			int actualNumber = questionInfo.ActualNumber;
			ObjectColor targetObjectColor = questionInfo.TargetColor;
			ObjectShape shapeToFind = questionInfo.ShapeToFind;
			int randomObjectCount = questionInfo.RandomObjectCount;

			Color targetCorrectColor = DATA.GetColorByColorInfo(targetObjectColor);
			int[] objectIndexArrayToSetCorrectColor = new int [actualNumber];
			ColoredObject[] targetCorrectColoredObjects = new ColoredObject[actualNumber]; 
			targetCorrectColoredObjects = GetColoredObjectByShape(shapeToFind);
			
			objectIndexArrayToSetCorrectColor = GetRandomNumberArray(actualNumber, targetCorrectColoredObjects.Length);
			
			SetObjectGroupsColors(targetCorrectColoredObjects,objectIndexArrayToSetCorrectColor,targetCorrectColor);
			
			SetRandomColorForRest(shapeToFind, targetObjectColor, randomObjectCount);
		}

		private void SetRandomColorForRest(ObjectShape correctObjectShape, ObjectColor correctObjectColor,int randomObjectCount)
		{
			int[] randomObjectIndexGroupOne =  new int [randomObjectCount/2];
			int[] randomObjectIndexGroupTwo =  new int [randomObjectCount/2];
					
			ColoredObject[] targetObjectGroupOne;
			ColoredObject[] targetObjectGroupTwo;
			
			ObjectColor[] wrongColors = GetRandomInCorrectColors(correctObjectColor);
			switch (correctObjectShape)
			{
				case ObjectShape.Pyramid:
					randomObjectIndexGroupOne = GetRandomNumberArray(randomObjectCount/2, GetColoredObjectByShape(ObjectShape.Cube).Length);
					randomObjectIndexGroupTwo = GetRandomNumberArray(randomObjectCount/2, GetColoredObjectByShape(ObjectShape.Sphere).Length);
					
					targetObjectGroupOne = GetColoredObjectByShape(ObjectShape.Cube);
					targetObjectGroupTwo = GetColoredObjectByShape(ObjectShape.Sphere);
					
					SetObjectGroupsColors(targetObjectGroupOne,randomObjectIndexGroupOne,DATA.GetColorByColorInfo(wrongColors[0]));
					SetObjectGroupsColors(targetObjectGroupTwo,randomObjectIndexGroupTwo,DATA.GetColorByColorInfo(wrongColors[1]));
					break;
				case ObjectShape.Cube:
					randomObjectIndexGroupOne = GetRandomNumberArray(randomObjectCount/2, GetColoredObjectByShape(ObjectShape.Cube).Length);
					randomObjectIndexGroupTwo = GetRandomNumberArray(randomObjectCount/2, GetColoredObjectByShape(ObjectShape.Sphere).Length);
					
					targetObjectGroupOne = GetColoredObjectByShape(ObjectShape.Pyramid);
					targetObjectGroupTwo = GetColoredObjectByShape(ObjectShape.Sphere);
					
					SetObjectGroupsColors(targetObjectGroupOne,randomObjectIndexGroupOne,DATA.GetColorByColorInfo(wrongColors[0]));
					SetObjectGroupsColors(targetObjectGroupTwo,randomObjectIndexGroupTwo,DATA.GetColorByColorInfo(wrongColors[1]));
					break;
					
				case ObjectShape.Sphere:
					randomObjectIndexGroupOne = GetRandomNumberArray(randomObjectCount/2, GetColoredObjectByShape(ObjectShape.Cube).Length);
					randomObjectIndexGroupTwo = GetRandomNumberArray(randomObjectCount/2, GetColoredObjectByShape(ObjectShape.Sphere).Length);
					
					targetObjectGroupOne = GetColoredObjectByShape(ObjectShape.Pyramid);
					targetObjectGroupTwo = GetColoredObjectByShape(ObjectShape.Cube);
					
					SetObjectGroupsColors(targetObjectGroupOne,randomObjectIndexGroupOne,DATA.GetColorByColorInfo(wrongColors[0]));
					SetObjectGroupsColors(targetObjectGroupTwo,randomObjectIndexGroupTwo,DATA.GetColorByColorInfo(wrongColors[1]));
					
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(correctObjectShape), correctObjectShape, null);
			}
			
		}

		private void SetObjectGroupsColors(ColoredObject[] objectsToColorSet,int[] randomIndexes,Color targetColor)
		{
			for (int i = 0; i < randomIndexes.Length; i++)
			{
				objectsToColorSet[randomIndexes[i]].SetColor(targetColor);
			}
		}

		// 

		private ColoredObject[] GetColoredObjectByShape(ObjectShape objShape)
		{
			for (int i = 0; i < shapeAndBelongins.Length; i++)
			{
				if (objShape == shapeAndBelongins[i].RepresentiveShape)
				{
					return shapeAndBelongins[i].ColoredObjects;
				}
			}

			return null;
		}
		

		private int[] GetRandomNumberArray(int returnArraySize, int randomIndexRange)
		{
			HashSet<int> uniqueNumbers = new HashSet<int>();

			while (uniqueNumbers.Count < returnArraySize)
			{
				int randomNumber = Random.Range(0, randomIndexRange);
				uniqueNumbers.Add(randomNumber);
			}

			int[] randomNumbersArray = new int[returnArraySize];
			uniqueNumbers.CopyTo(randomNumbersArray);

			return randomNumbersArray;
		}

		private ObjectColor[] GetRandomInCorrectColors(ObjectColor correctObjectColor)
		{
			List<ObjectColor> allObjectColors = new List<ObjectColor>();

			foreach (var ColorData in DATA.ColorToSetDatas)
			{
				allObjectColors.Add(ColorData.ObjectColorInfo);	
			}

			allObjectColors.Remove(correctObjectColor);
			
			
			ObjectColor[] wrongColors = new ObjectColor[2];
			
			allObjectColors.CopyTo(wrongColors);

			return wrongColors;
		}
		
	}
}
