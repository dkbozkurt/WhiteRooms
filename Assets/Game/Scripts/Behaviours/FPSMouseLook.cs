using Cinemachine;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	public class FPSMouseLook : MonoBehaviour
	{
		[SerializeField] private CinemachineVirtualCamera _playerCamera;
		[SerializeField] private float mouseSensitivity = 1000f;

		private float yRotation = 0f;

		private bool _rotationCamUseable = false;
		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				_rotationCamUseable = true;
			}
			
			if (_rotationCamUseable)
			{
				LookAround();	
			}
		}

		private void LookAround()
		{
			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
			LookAroundAlongY(mouseY);
			LookAroundAlongX(mouseX);
		}

		private void LookAroundAlongX(float mouseX)
		{
			transform.Rotate(Vector3.up * mouseX);
		}

		private void LookAroundAlongY(float mouseY)
		{
			yRotation -= mouseY;
        
			yRotation = Mathf.Clamp(yRotation, -90f, 90f);
			Debug.Log(yRotation);
			_playerCamera.transform.localRotation =Quaternion.Euler(yRotation,0f,0f);
        
		}
	}
}
