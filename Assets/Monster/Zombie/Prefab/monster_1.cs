using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster_1 : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    public float stoppingDistance = 10f; // 멈추는 거리
    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("player");
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (nav.destination != target.transform.position)
        {
            nav.SetDestination(target.transform.position);
        }
        else
        {
            nav.SetDestination(transform.position);
        }
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < stoppingDistance)
        {
            // 플레이어를 추격 중인 경우 애니메이션을 재생
            animator.SetBool("run", true);
            nav.isStopped = false;
        }
        else
        {
            // 플레이어를 추격하지 않는 경우 애니메이션을 정지
            animator.SetBool("run", false);
            nav.isStopped = true;

        }
    }
}