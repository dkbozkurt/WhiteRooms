using UnityEngine;

namespace Game.Scripts.Helpers
{
	public static class Utilities
	{
		private static Camera _camera;
        
		public static Camera Camera
		{
			get
			{
				if(_camera == null) _camera = UnityEngine.Camera.main;
				return _camera;
			}    
		}
	}
}
