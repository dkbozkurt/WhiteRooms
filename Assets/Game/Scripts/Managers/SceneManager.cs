using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Managers
{
	public class SceneManager : SingletonBehaviour<SceneManager>
	{
		[SerializeField] private float _screenCallDuration = 0.5f;

		[SerializeField] private Image _background;
		
		[Header("StartScreen")] 
		[SerializeField] private Image _startScreenImage;
		private bool _startScreenOpen;

		[Header("LoseScreen")] 
		[SerializeField] private Image _loseScreenImage;
		private bool _loseScreenOpen;
		
		[Header("WinScreen")] 
		[SerializeField] private Image _winScreenImage;
		private bool _winScreenOpen;

		private Color alphaColor = new Color(1f, 1f, 1f, 0f);
		private void Awake()
		{
			ResetAll();
			CallStartScreen();
		}

		protected override void OnAwake()
		{ }

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
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (_startScreenOpen)
				{
					StartGame();
				}
				else if (_winScreenOpen || _loseScreenOpen)
				{
					RestartScene();
				}
			}
		}

		public void CallStartScreen()
		{
			_startScreenImage.DOColor(Color.white, _screenCallDuration).SetEase(Ease.Linear).OnComplete(() =>
			{
				_startScreenOpen = true;
			});
		}
		
		public void CallWinScreen()
		{
			_winScreenImage.DOColor(Color.white, _screenCallDuration).SetEase(Ease.Linear).OnComplete(() =>
			{
				_winScreenOpen = true;
			});
		}
		
		public void CallLoseScreen()
		{
			_loseScreenImage.DOColor(Color.white, _screenCallDuration).SetEase(Ease.Linear).OnComplete(() =>
			{
				_loseScreenOpen = true;
			});
		}
		
		public void RestartScene()
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		public void StartGame()
		{
			_startScreenOpen = false;
			_background.DOColor(alphaColor, _screenCallDuration).SetEase(Ease.Linear);
			
			_startScreenImage.DOColor(alphaColor, _screenCallDuration).SetEase(Ease.Linear).OnComplete(() =>
			{
				GameManager.Instance.StartGame();	
			});
		}

		public void ResetAll()
		{
			_startScreenOpen = false;
			_loseScreenOpen = false;
			_winScreenOpen = false;
			
			_background.color = new Color(1f,1f,1f,1f);
			_startScreenImage.color = new Color(1f,1f,1f,0f);
			_loseScreenImage.color = new Color(1f,1f,1f,0f);
			_winScreenImage.color = new Color(1f,1f,1f,0f);
			
		}
	}
}
