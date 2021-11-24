using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Defense.Player
{
    public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
    {
        [Header("Camera")]
        public Camera cam;
        public Vector3 cameraOffset = new Vector3(3, 5, 4);
        private RaycastHit hit;
        public GameObject clickEffect;
        
        [Header("Player")]
        public GameObject player;
        public float moveSpeed;
        public float angularSpeed;
        private NavMeshAgent playerNav;
        private Animator playerAnimator;

        private static readonly int Walk = Animator.StringToHash("Walk");


        private void Start()
        {
            playerNav = player.GetComponent<NavMeshAgent>();
            playerNav.speed = moveSpeed;
            playerNav.angularSpeed = angularSpeed;
            playerAnimator = player.GetComponent<Animator>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // if(Input.GetMouseButtonDown(0)) return;
            Ray ray = cam.ScreenPointToRay(eventData.position);
            Physics.Raycast(ray, out hit);

            if (null != hit.transform)
            {
                clickEffect.SetActive(false);
                clickEffect.transform.position = hit.point;
                clickEffect.SetActive(true);

                if (hit.transform.gameObject.CompareTag("Land"))
                {
                    playerNav.SetDestination(hit.point);
                    //print(player.transform.position.ToString());
                }
            }
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            print("마우스이동");
            // if(Input.GetMouseButtonDown(0)) return;
            Ray ray = cam.ScreenPointToRay(eventData.position);
            Physics.Raycast(ray, out hit);

            if (null != hit.transform)
            {
                clickEffect.transform.position = hit.point;

                if (hit.transform.gameObject.CompareTag("Land"))
                    playerNav.SetDestination(hit.point);
            }
        }

        private void Update()
        {
            playerAnimator.SetBool(Walk, playerNav.velocity != Vector3.zero);
            
            var position = player.transform.position;
            cam.transform.position = position + cameraOffset;
            cam.transform.LookAt(position);
        }

    }
}
