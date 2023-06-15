using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private Image _crosshair;
        public LayerMask _detectionLayerMask;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.CanInteract)
            {
                SendRayAndDetect();
            }
        }
        
        private void SendRayAndDetect()
        {
            var ray = new Ray(_playerCamera.position, _playerCamera.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100,_detectionLayerMask))
            {
                if (hit.transform.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    interactable.OnButtonSelect();
                }
            }
        }
    }
}