using System;
using Cinemachine;
using Game.Scripts.Helpers;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	public class FPSMouseLook : MonoBehaviour
	{
		[SerializeField] private float mouseSensitivity = 1000f;

		private Camera _mainCamera;
		private float yRotation = 0f;

		private void Awake()
		{
			_mainCamera = Utilities.Camera;
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		private void Update()
		{
			if(!GameManager.Instance.CanLookAround) return;

			LookAround();	
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
			_mainCamera.transform.localRotation =Quaternion.Euler(yRotation,0f,0f);
        
		}
	}
}
