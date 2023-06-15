
using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private Image _crosshair;
        public LayerMask _detectionLayerMask;

        private bool CanDetectButtons = true;

        private void Start()
        {
            SetButtonDetection(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && CanDetectButtons)
            {
                SendRayAndDetect();
            }
        }

        private void SetButtonDetection(bool status)
        {
            CanDetectButtons = status;
        }

        private void SendRayAndDetect()
        {
            var ray = new Ray(_playerCamera.position, _playerCamera.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100,_detectionLayerMask))
            {
                Debug.Log("Name : " + hit.transform.gameObject.name);
                // if (hit.transform.gameObject.TryGetComponent(out IDetectable))
                // {
                //     
                // }
            }
        }
    }
}