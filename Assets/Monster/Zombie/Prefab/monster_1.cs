using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster_2 : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    public float stoppingDistance = 20f; // 멈추는 거리
    private float getPlayerDistance = 5f; // 플레이어를 잡는 거리
    private float slowspeed = 2.5f; // 느리게 적용할 속도값
    private float originalSpeed; // NavMeshAgent의 원래 속도
    private bool isSpeedModified = false; // 속도가 감소되었는지 여부를 나타내는 플래그

    public AudioClip getPlayerClip; // getPlayer 오디오 클립
    public AudioClip playerScreamingClip; // playerScreaming 오디오 클립
    public AudioClip surprisedSoundClip; // surprisedSound 오디오 클립
    public AudioClip badEndingsongClip; // badEndingsong 오디오 클립
    private AudioSource audioSource; // 음악 재생용 AudioSource
    private AudioSource getPlayerAudioSource; // getPlayer 오디오 클립을 재생하는 AudioSource
    private AudioSource playerScreamingAudioSource; // playerScreaming 오디오 클립을 재생하는 AudioSource
    private AudioSource surprisedSoundAudioSource; // surprisedSound 오디오 클립을 재생하는 AudioSource
    private AudioSource badEndingsongAudioSource; // badEndingsong 오디오 클립을 재생하는 AudioSource

    private bool hasPlayedAudio = false; // 오디오가 이미 재생되었는지를 나타내는 플래그

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
        originalSpeed = nav.speed; // NavMeshAgent의 원래 속도 저장

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
            if (!isSpeedModified)
            {
                nav.speed = slowspeed;
                isSpeedModified = true;
                StartCoroutine(RestoreNavSpeed(5f)); // 원래 속도로 복귀하는 함수 5초뒤 호출
            }
            
            // 플레이어를 추격 중인 경우 애니메이션을 재생
            animator.SetBool("run", true);
            nav.isStopped = false;

            if (distanceToTarget < getPlayerDistance && !hasPlayedAudio)
            {
                animator.SetBool("atack", true);

                // 플레이어의 카메라를 바라보도록 회전
                Vector3 targetPosition = new Vector3(nav.transform.position.x, nav.transform.position.y + 2, nav.transform.position.z);
                target.GetComponentInChildren<Camera>().transform.LookAt(targetPosition);
                audioSource.Stop();
                // 오디오 클립들을 동시에 재생
                Debug.Log("음악 재생");
                getPlayerAudioSource.PlayOneShot(getPlayerClip);
                playerScreamingAudioSource.PlayOneShot(playerScreamingClip);
                surprisedSoundAudioSource.PlayOneShot(surprisedSoundClip);
                badEndingsongAudioSource.PlayOneShot(badEndingsongClip);
                Debug.Log("음악 재생");
                getPlayerAudioSource.PlayOneShot(getPlayerClip);
                playerScreamingAudioSource.PlayOneShot(playerScreamingClip);
                surprisedSoundAudioSource.PlayOneShot(surprisedSoundClip);
                badEndingsongAudioSource.PlayOneShot(badEndingsongClip);

                hasPlayedAudio = true; // 오디오가 재생되었음을 표시
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

    IEnumerator RestoreNavSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);

        nav.speed = originalSpeed; // 원래의 NavMeshAgent 속도로 복원
        Debug.Log("원래속도 적용");
        isSpeedModified = true;
    }
}

