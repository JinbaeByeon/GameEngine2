using System;
using Defense;
using Defense.Manager;
using DevionGames;
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

        public int maxHp;
        public int curHp;
        
        private Rigidbody rigid;
        private BoxCollider boxCollider;

        void Start()
        {
            print("Start 불림");
            target = Waypoints.points[0];
            EventManager.Instance.On("onGameReStart",OnGameReStart);
        }

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag($"Bullet"))
            {
                curHp -= 10;
            }
        }

        void OnGameReStart(object param)
        {
            gameObject.SetActive(false);
            waypointIndex=0;
            target = Waypoints.points[0];
            curHp = maxHp;
            transform.position = new Vector3(0, 0, 0);
        }

        bool IsDead()
        {
            if (curHp <= 0)
                return true;
            return false;
        }

        void Update()
        {
            //플레이어한테 잡히면 지움
            if (IsDead())
            {
                gameObject.SetActive(false);
                waypointIndex=0;
                target = Waypoints.points[0];
                curHp = maxHp;
                transform.position = new Vector3(0, 100, 0);
                //Destroy(gameObject);
                // 몬스터가 죽으면 카운터 추가
                MonsterCount.KillCnt += 1;
                return;
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
                // gameObject.SetActive(false);
                waypointIndex=0;
                target = Waypoints.points[0];
                return;
            }

            waypointIndex = ++waypointIndex % 4;
            target = Waypoints.points[waypointIndex];
            transform.LookAt(target);

        }
        
    }
}
