using Cinemachine;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	public class FPSMouseLook : MonoBehaviour
	{
		[SerializeField] private CinemachineVirtualCamera _playerCamera;
		[SerializeField] private float mouseSensitivity = 1000f;

		private float yRotation = 0f;
    
		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		private void Update()
		{
			LookAround();
		}

		// Looks around and rotates body.
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
        
			// To avoid player to look all around its body along y
			yRotation = Mathf.Clamp(yRotation, -90f, 90f);
			_playerCamera.transform.localRotation =Quaternion.Euler(yRotation,0f,0f);
        
		}
	}
}
