using System;
using DG.Tweening;
using Game.Scripts.Helpers;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	public class ColoredObject : MonoBehaviour
	{
		private Renderer _renderer;
		private float _materialSwapDuration = 0.2f; 

		private void OnEnable()
		{
			
		}

		private void OnDisable()
		{
			
		}

		private void Start()
		{
			_renderer = GetComponent<Renderer>();
			SetColorWithoutDelay(GameManager.Instance.DATA.DefaultObjectColor);
		}
		
		// private void SetMaterial(Material targetMat)
		// {
		// 	_renderer.material = targetMat;
		// }

		private void SetColor(Color targetColor)
		{
			_renderer.DOKill();
			_renderer.material.DOColor(targetColor,_materialSwapDuration).SetEase(Ease.Linear);
		}

		private void SetColorWithoutDelay(Color targetColor)
		{
			_renderer.material.color = targetColor;
		}
		
	}
}


