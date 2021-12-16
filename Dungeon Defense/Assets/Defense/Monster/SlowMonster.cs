using Defense;
using Defense.Manager;
using UnityEngine;

namespace Monster
{
    public class SlowMonster : MonoBehaviour
    {
        private float MoveSpeed = 3f;

        private Transform target;
        private int waypointIndex = 0;
    
        public int maxHp;
        public int curHp;
        
        private Rigidbody rigid;
        private BoxCollider boxCollider;

        void Start()
        {
            EventManager.Instance.On("onGameReStart",OnGameReStart);
        }
        void Awake()
        {
            target = Waypoints.points[0];
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
        bool isDead()
        {
            if (curHp <= 0)
                return true;
            return false;
        }

        void OnGameReStart(object param)
        {
            gameObject.SetActive(false);
            waypointIndex=0;
            target = Waypoints.points[0];
            curHp = maxHp;
            transform.position = new Vector3(0, 0, 0);
        }
        void Update()
        {
            //플레이어한테 잡히면 지움
            if (isDead())
            {
                gameObject.SetActive(false);
                waypointIndex=0;
                target = Waypoints.points[0];
                curHp = maxHp;
                transform.position = new Vector3(0, 100, 0);
                
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