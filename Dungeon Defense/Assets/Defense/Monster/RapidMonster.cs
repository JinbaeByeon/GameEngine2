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
        private bool isTurn = true;

        void Start()
        {
            print("Start 불림");
            target = Waypoints.points[0];
        }
        

        bool isDead()
        {
            return false;
        }

        void Update()
        {
            //플레이어한테 잡히면 지움
            if (isDead())
            {
                //Destroy(gameObject);
                // 몬스터가 죽으면 카운터 추가
                MonsterCount.MonsterCnt += 1;
            }

            Vector3 dir = target.position - transform.position;

            transform.position += dir.normalized * MoveSpeed * Time.deltaTime;

            
            
            transform.LookAt(target);
            
            
            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
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
