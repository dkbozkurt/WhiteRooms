using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
	public class FPSMovement : MonoBehaviour
	{ 
        private CharacterController _characterController;

        [Header("General Variables")]
        [SerializeField] private float speed = 12f;
        private Vector3 currentVelocity;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }
    
        private void Update()
        {
            if (GameManager.Instance.CanMove)
            {
                Movement();    
            }
            
        }
    
        private void Movement()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
    
            Vector3 move =transform.right * x + transform.forward * z;
            _characterController.Move(move * speed * Time.deltaTime);
        }
    }
}
