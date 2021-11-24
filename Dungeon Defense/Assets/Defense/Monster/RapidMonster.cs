using System;
using Defense;
using Unity.VisualScripting;
using UnityEngine;

namespace Monster
{
    public class RapidMonster : MonoBehaviour
    {
        private float MoveSpeed = 5f;

        private Transform target;
        private int waypointIndex = 0;

        private float JumpPower = 30f;
        private bool isJumping;
    
        void Start()
        {
            print("Start 불림");
            target = Waypoints.points[0];

            isJumping = false;
        }


        private void OnEnable()
        {
            //Invoke("Dead", 3);
        }

        void Dead()
        {
            gameObject.SetActive(false);
        }

        void Jump()
        {
            if (!isJumping)
            {
                isJumping = true;
                transform.Translate(Vector3.up * Time.deltaTime * JumpPower);
            }
            else
            {
                return;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Land"))
                isJumping = false;
        }

        bool isDead()
        {
            return false;
        }

        void Update()
        {
            // Jump();
            
            //플레이어한테 잡히면 지움
            if (isDead())
                Destroy(gameObject);
            
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime);
            
            
            
            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                
                GetNextWaypoint();
                //transform.LookAt(target);
            }
        }

        void GetNextWaypoint()
        {
            // 결승선에 들어옴
            if (waypointIndex >= Waypoints.points.Length -1)
            {
                // 선을 넘으면 죽진 않고 Active만 꺼놓음
                gameObject.SetActive(false);
                waypointIndex=0;
                target = Waypoints.points[0];
                return;
            }
            
            
            waypointIndex = ++waypointIndex % 4;
            target = Waypoints.points[waypointIndex];
            
            
        }
        
    }
}
