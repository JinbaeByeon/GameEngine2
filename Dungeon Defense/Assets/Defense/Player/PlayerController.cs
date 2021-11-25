using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Defense.Player
{
    public class PlayerController : MonoBehaviour, IPointerDownHandler
    {
        [Header("Camera")]
        public Camera cam;
        public Vector3 cameraOffset = new Vector3(3, 5, 4);
        private RaycastHit hit;
        public GameObject clickEffect;
        private float zoom = 1;

        [Header("Player")]
        public GameObject player;
        public float moveSpeed;
        public float angularSpeed;
        private NavMeshAgent playerNav;
        private Animator playerAnimator;

        [Header("Ground")] 
        public GameObject ground;
        public GameObject lGround;
        public GameObject rGround;
        public GameObject tGround;
        public GameObject bGround;

        private static readonly int Walk = Animator.StringToHash("Walk");
        private CharacterController _characterController;


        private void Start()
        {
            playerNav = player.GetComponent<NavMeshAgent>();
            playerNav.speed = moveSpeed;
            playerNav.angularSpeed = angularSpeed;
            playerAnimator = player.GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // if(Input.GetMouseButtonDown(0)) return;
            // Ray ray = cam.ScreenPointToRay(eventData.position);
            // Physics.Raycast(ray, out hit);
            //
            //
            // if (null != hit.transform)
            // {
            //     clickEffect.SetActive(false);
            //     clickEffect.transform.position = hit.point;
            //     clickEffect.SetActive(true);
            //
            //     if (hit.transform.gameObject.CompareTag("Land"))
            //     {
            //         playerNav.SetDestination(hit.point);
            //         print(player.transform.position.ToString());
            //     }
            // }
        }

        private void Update()
        {
            playerAnimator.SetBool(Walk, playerNav.velocity != Vector3.zero);
            
            var position = player.transform.position;
            cam.transform.position = position + cameraOffset * zoom;
            cam.transform.LookAt(position);
        }

        public void Zoom(InputAction.CallbackContext ctx)
        {
            float z = ctx.ReadValue<float>();
            
            if (z < 0 && zoom < 5.0f) 
                zoom *= 1.05f;
            else if (z > 0 && zoom > 0.3f) 
                zoom *= 0.95f;
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            Mouse mouse = Mouse.current;
            Vector2 position = mouse.position.ReadValue();
            print("포지션: " +position);
            Ray ray = cam.ScreenPointToRay(position);
            Physics.Raycast(ray, out hit);
            
            
            if (null != hit.transform)
            {
                clickEffect.SetActive(false);
                clickEffect.transform.position = hit.point;
                clickEffect.SetActive(true);
            
                if (hit.transform.gameObject.CompareTag("Land"))
                {
                    playerNav.SetDestination(hit.point);
                    print(player.transform.position.ToString());
                }
            }
        }

    }
}
