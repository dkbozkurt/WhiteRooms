using System;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Managers
{
	public class GameManager : MonoBehaviour
	{
		public static event Action OnGameStart;
		public static event Action OnTutorialEnd;
		
		public GameDataScriptableObject DATA;

		private int _sectionNumber = 0;
		
		private void Awake()
		{
        
		}
	
		private void OnEnable()
		{
        
		}
	
		private void OnDisable()
		{
        
		}	
	
		private void Start()
		{
			
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				OnGameStart?.Invoke();
				Debug.Log(DATA.GetSectionQuestion(0));
			}
		}
	}
}
