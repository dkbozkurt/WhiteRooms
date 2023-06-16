using System;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	public class ColoredObject : MonoBehaviour
	{
		private Renderer _renderer;

		private void Start()
		{
			_renderer = GetComponent<Renderer>();
		}
		
		private void SetMaterial(Material targetMat)
		{
			_renderer.material = targetMat;
		}
	}
}


