using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Defense.Player
{
    public class PlayerController : MonoBehaviour
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
        public GameObject weapon;
        public GameObject hand;
        private List<GameObject> bullets;
        private float attackTime =0;

        [Header("Ground")] 
        public GameObject ground;
        public GameObject lGround;
        public GameObject rGround;
        public GameObject tGround;
        public GameObject bGround;

        private static readonly int walk = Animator.StringToHash("walk");
        private static readonly int attack = Animator.StringToHash("attack");
        private CharacterController _characterController;


        private void Start()
        {
            playerNav = player.GetComponent<NavMeshAgent>();
            playerNav.speed = moveSpeed;
            playerNav.angularSpeed = angularSpeed;
            playerAnimator = player.GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            bullets = new List<GameObject>();
        }
        private void Update()
        {
            playerAnimator.SetBool(walk, playerNav.velocity != Vector3.zero);
            
            var position = player.transform.position;
            cam.transform.position = position + cameraOffset * zoom;
            cam.transform.LookAt(position);

            foreach (var bullet in bullets)
            {
                Vector3 v = bullet.transform.forward;
                print("bullet - " + v.ToString());
                bullet.transform.position += 5 * v * Time.deltaTime;
            }
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
                    // print(player.transform.position.ToString());
                }
            }
        }

        public void Attack(InputAction.CallbackContext ctx)
        {   
            Mouse mouse = Mouse.current;
            Vector2 position = mouse.position.ReadValue();
            
            Ray ray = cam.ScreenPointToRay(position);
            Physics.Raycast(ray, out hit);

            if (null != hit.transform)
            {
                if (hit.transform.gameObject.CompareTag("Land"))
                {
                    Vector3 lookAt = hit.point;
                    lookAt.y = player.transform.position.y;
                    player.transform.LookAt(lookAt);
                    
                    playerAnimator.SetBool(walk, false);
                    playerAnimator.SetTrigger(attack);
                    if (weapon.name == "Stone")
                    {
                        GameObject stone = Instantiate(weapon);
                        stone.transform.position = hand.transform.position;
                        lookAt.y = stone.transform.position.y;
                        print("player - " + player.transform.forward.ToString());

                        stone.transform.forward = player.transform.forward;
                        bullets.Add(stone);
                    }
                    playerNav.SetDestination(player.transform.position);
                }
            }
        }

    }
}