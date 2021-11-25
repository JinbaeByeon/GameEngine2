<<<<<<< HEAD
using Defense;
using UnityEngine;

namespace Monster
{
    public class SlowMonster : MonoBehaviour
    {
        private float MoveSpeed = 3f;

        private Transform target;
        private int waypointIndex = 0;
    
        void Awake()
        {
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
                gameObject.SetActive(false);
                // Destroy(gameObject);   
            }
            
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime);
            
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
=======
using Defense;
using UnityEngine;

namespace Monster
{
    public class SlowMonster : MonoBehaviour
    {
        private float MoveSpeed = 3f;

        private Transform target;
        private int waypointIndex = 0;
    
        void Awake()
        {
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
                gameObject.SetActive(false);
                // Destroy(gameObject);   
            }
            
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime);
            
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
>>>>>>> 2882221cd0f916c641c439b59900cb1dfc42ad8d
