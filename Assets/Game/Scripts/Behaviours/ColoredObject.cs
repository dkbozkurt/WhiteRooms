using DG.Tweening;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	public class ColoredObject : MonoBehaviour
	{
		private Renderer _renderer;
		private float _materialSwapDuration = 0.25f;

		private bool IsColorChanged = false;
		
		private void OnEnable()
		{
			GameManager.OnColorResetCall += ResetColor;
		}

		private void OnDisable()
		{
			GameManager.OnColorResetCall -= ResetColor;
		}

		private void Start()
		{
			_renderer = GetComponent<Renderer>();
			SetColorToDefault();
		}
		
		private void SetColorToDefault()
		{
			Color targetColor = GameManager.Instance.DATA.DefaultObjectColor;
			IsColorChanged = false;
			// _renderer.material.color = GameManager.Instance.DATA.DefaultObjectColor;
			_renderer.DOKill();
			_renderer.material.DOColor(targetColor,_materialSwapDuration).SetEase(Ease.Linear);
		}
		
		public void SetColor(Color targetColor)
		{
			IsColorChanged = true;
			_renderer.DOKill();
			_renderer.material.DOColor(targetColor,_materialSwapDuration).SetEase(Ease.Linear);
		}

		private void ResetColor()
		{
			if(IsColorChanged == false) return;
			SetColorToDefault();
		}

		
		
	}
}


