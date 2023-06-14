using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster_1 : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    public float stoppingDistance = 20f; // 멈추는 거리
    private bool isNavFrozen = false; // NavMeshAgent를 움직이지 않게 하는 플래그
    private float navFreezeDuration = 4f; // NavMeshAgent를 얼린 시간
    private float getPlayerDistance = 3.5f; // 플레이어를 잡는 거리

    public AudioClip getPlayerClip; // getPlayer 오디오 클립
    public AudioClip playerScreamingClip; // playerScreaming 오디오 클립
    public AudioClip surprisedSoundClip; // surprisedSound 오디오 클립
    public AudioClip badEndingsongClip; // badEndingsong 오디오 클립
    private AudioSource audioSource; // 음악 재생용 AudioSource
    private AudioSource getPlayerAudioSource; // getPlayer 오디오 클립을 재생하는 AudioSource
    private AudioSource playerScreamingAudioSource; // playerScreaming 오디오 클립을 재생하는 AudioSource
    private AudioSource surprisedSoundAudioSource; // surprisedSound 오디오 클립을 재생하는 AudioSource
    private AudioSource badEndingsongAudioSource; // badEndingsong 오디오 클립을 재생하는 AudioSource

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("player");
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기

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
            // 플레이어를 추격 중인 경우 애니메이션을 재생
            animator.SetBool("run", true);
            nav.isStopped = false;

            if (distanceToTarget < getPlayerDistance)
            {
                animator.SetBool("atack", true);

                // 플레이어의 카메라를 바라보도록 회전
                Vector3 targetPosition = new Vector3(nav.transform.position.x, nav.transform.position.y + 2, nav.transform.position.z);
                target.GetComponentInChildren<Camera>().transform.LookAt(targetPosition);
                audioSource.Stop();
                // 오디오 클립들을 동시에 재생
                getPlayerAudioSource.PlayOneShot(getPlayerClip);
             //   playerScreamingAudioSource.PlayOneShot(playerScreamingClip);
               // surprisedSoundAudioSource.PlayOneShot(surprisedSoundClip);
               // badEndingsongAudioSource.PlayOneShot(badEndingsongClip);
            }
        }
        else
        {
            // 플레이어를 추격하지 않는 경우 애니메이션을 정지
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
