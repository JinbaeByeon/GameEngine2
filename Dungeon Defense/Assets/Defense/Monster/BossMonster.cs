using System;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
using UnityEngine.AI;

namespace Defense.Monster
{
    public class BossMonster : MonoBehaviour
    {
        public Rigidbody rigid;
        public BoxCollider boxCollider;
        public NavMeshAgent nav;
        public Animator anim;
        public MeshRenderer[] _meshRenderers;

        public Transform target;
        
        private Vector3 lookVec;
        private bool isAttack;
        private bool isSkill;
        
        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            nav = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            _meshRenderers = GetComponentsInChildren<MeshRenderer>();

            isAttack = true;
            isSkill = true;
            //StartCoroutine(Think());
        }

        // Update is called once per frame
        void Update()
        {
            // 플레이어를 따라가게
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * 3f * Time.deltaTime);
            
            // 플레이어를 바라보게
            transform.LookAt(target);

            float toPlayerDistance = DistanceToPoint(target.position, transform.position);
            
            // 가까워지면 공격
            if (toPlayerDistance < 5f)
            {
                BossAttack();
                
                // 가까워지면 이거 초기화해서 다시 멀어지면 스킬쓰게
                isSkill = true;
            }
            else
            {
                isAttack = true;
            }
            
            // 일정 거리 이상 멀어지면 일정 시간마다 스킬 공격
            if (toPlayerDistance > 15f)
            {
                BossSkillAttack();
            }
        }

        void BossAttack()
        {
            if (isAttack)
                anim.SetTrigger("doAttack");
            isAttack = false;
        }

        void BossSkillAttack()
        {
            if (isSkill)
                anim.SetTrigger("doSkill");
            isSkill = false;
        }
        
        public float DistanceToPoint(Vector3 a, Vector3 b)
        {
            return (float)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.z - b.z, 2));
        }

        IEnumerator OnDamage(Vector3 reactVec)
        {
            foreach (MeshRenderer mesh in _meshRenderers)
                mesh.material.color = Color.red;

            yield return new WaitForSeconds(0.1f);
        }

        // IEnumerator Think()
        // {
        //     yield return new WaitForSeconds(0.1f);
        //
        //     int ranAction = UnityEngine.Random.Range(0, 3);
        //     switch (ranAction)
        //     {
        //         case 0:
        //             // 공격 패턴
        //             StartCoroutine(Attack());
        //             break;
        //         case 1:
        //             // 스킬 패턴
        //             StartCoroutine(Skill());
        //             break;
        //         case 2:
        //             // 스턴 패턴
        //             StartCoroutine(Taunt());
        //             break;
        //     }
        // }
        //
        // IEnumerator Attack()
        // {
        //     anim.SetTrigger("doAttack");
        //     yield return new WaitForSeconds(2.5f);
        //     
        //     StartCoroutine(Think());
        // }
        //
        // IEnumerator Skill()
        // {
        //     anim.SetTrigger("doSkill");
        //     yield return new WaitForSeconds(10f);
        //  
        //     //StartCoroutine(Think());
        // }
        //
        // IEnumerator Taunt()
        // {
        //     anim.SetTrigger("doDamage");
        //     yield return new WaitForSeconds(2.5f);
        //     
        //     StartCoroutine(Think());
        // }
    }
}
