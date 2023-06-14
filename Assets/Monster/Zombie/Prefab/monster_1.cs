using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster_1 : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    public float stoppingDistance = 10f; // ���ߴ� �Ÿ�
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
            // �÷��̾ �߰� ���� ��� �ִϸ��̼��� ���
            animator.SetBool("run", true);
            nav.isStopped = false;
        }
        else
        {
            // �÷��̾ �߰����� �ʴ� ��� �ִϸ��̼��� ����
            animator.SetBool("run", false);
            nav.isStopped = true;

        }
    }
}