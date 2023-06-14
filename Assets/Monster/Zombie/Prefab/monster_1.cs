using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster_1 : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    public float stoppingDistance = 20f; // ���ߴ� �Ÿ�
    private bool isNavFrozen = false; // NavMeshAgent�� �������� �ʰ� �ϴ� �÷���
    private float navFreezeDuration = 4f; // NavMeshAgent�� �� �ð�
    private float getPlayerDistance = 3.5f; // �÷��̾ ��� �Ÿ�

    public AudioClip getPlayerClip; // getPlayer ����� Ŭ��
    public AudioClip playerScreamingClip; // playerScreaming ����� Ŭ��
    public AudioClip surprisedSoundClip; // surprisedSound ����� Ŭ��
    public AudioClip badEndingsongClip; // badEndingsong ����� Ŭ��
    private AudioSource audioSource; // ���� ����� AudioSource
    private AudioSource getPlayerAudioSource; // getPlayer ����� Ŭ���� ����ϴ� AudioSource
    private AudioSource playerScreamingAudioSource; // playerScreaming ����� Ŭ���� ����ϴ� AudioSource
    private AudioSource surprisedSoundAudioSource; // surprisedSound ����� Ŭ���� ����ϴ� AudioSource
    private AudioSource badEndingsongAudioSource; // badEndingsong ����� Ŭ���� ����ϴ� AudioSource

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("player");
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������

        getPlayerAudioSource = gameObject.AddComponent<AudioSource>();
        getPlayerAudioSource.clip = getPlayerClip;

        playerScreamingAudioSource = gameObject.AddComponent<AudioSource>();
        playerScreamingAudioSource.clip = playerScreamingClip;

        surprisedSoundAudioSource = gameObject.AddComponent<AudioSource>();
        surprisedSoundAudioSource.clip = surprisedSoundClip;

        badEndingsongAudioSource = gameObject.AddComponent<AudioSource>();
        badEndingsongAudioSource.clip = badEndingsongClip;

        StartCoroutine(FreezeNavForDuration(navFreezeDuration));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNavFrozen)
        {
            if (nav.destination != target.transform.position)
            {
                nav.SetDestination(target.transform.position);
            }
            else
            {
                nav.SetDestination(transform.position);
            }
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < stoppingDistance)
        {
            // �÷��̾ �߰� ���� ��� �ִϸ��̼��� ���
            animator.SetBool("run", true);
            nav.isStopped = false;

            if (distanceToTarget < getPlayerDistance)
            {
                animator.SetBool("atack", true);

                // �÷��̾��� ī�޶� �ٶ󺸵��� ȸ��
                Vector3 targetPosition = new Vector3(nav.transform.position.x, nav.transform.position.y + 2, nav.transform.position.z);
                target.GetComponentInChildren<Camera>().transform.LookAt(targetPosition);
                audioSource.Stop();
                // ����� Ŭ������ ���ÿ� ���
                getPlayerAudioSource.PlayOneShot(getPlayerClip);
             //   playerScreamingAudioSource.PlayOneShot(playerScreamingClip);
               // surprisedSoundAudioSource.PlayOneShot(surprisedSoundClip);
               // badEndingsongAudioSource.PlayOneShot(badEndingsongClip);
            }
        }
        else
        {
            // �÷��̾ �߰����� �ʴ� ��� �ִϸ��̼��� ����
            animator.SetBool("run", false);
            nav.isStopped = true;

            animator.SetBool("atack", false);
        }
    }

    IEnumerator FreezeNavForDuration(float duration)
    {
        isNavFrozen = true;
        yield return new WaitForSeconds(duration);
        isNavFrozen = false;
    }
}
