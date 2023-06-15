using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class scavenger : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;
    public float stoppingDistance = 20f; // ���ߴ� �Ÿ�
    private float getPlayerDistance = 5.1f; // �÷��̾ ��� �Ÿ�
    private float slowspeed = 1.7f; // ������ ������ �ӵ���
    private float originalSpeed; // NavMeshAgent�� ���� �ӵ�
    private bool isSpeedModified = false; // �ӵ��� ���ҵǾ����� ���θ� ��Ÿ���� �÷���

    public AudioClip getPlayerClip; // getPlayer ����� Ŭ��
    public AudioClip playerScreamingClip; // playerScreaming ����� Ŭ��
    public AudioClip surprisedSoundClip; // surprisedSound ����� Ŭ��
    public AudioClip badEndingsongClip; // badEndingsong ����� Ŭ��
    private AudioSource audioSource; // ���� ����� AudioSource
    private AudioSource getPlayerAudioSource; // getPlayer ����� Ŭ���� ����ϴ� AudioSource
    private AudioSource playerScreamingAudioSource; // playerScreaming ����� Ŭ���� ����ϴ� AudioSource
    private AudioSource surprisedSoundAudioSource; // surprisedSound ����� Ŭ���� ����ϴ� AudioSource
    private AudioSource badEndingsongAudioSource; // badEndingsong ����� Ŭ���� ����ϴ� AudioSource

    private bool hasPlayedAudio = false; // ������� �̹� ����Ǿ������� ��Ÿ���� �÷���

    private bool isUIActive = false; // UI�� Ȱ��ȭ�Ǿ� �ִ��� ���θ� ��Ÿ���� �÷���
    private Coroutine uiCoroutine; // UI ������ �� ���̵� ��/�ƿ��� �����ϱ� ���� Coroutine ����

    public Canvas uiCanvas; // UI Canvas
    public GameObject deadImage; // ��� �̹���
    public GameObject retryButton; // ����� ��ư
    public float uiDelayTime = 3f; // UI ������ �ð�
    public float uiFadeDuration = 2f; // UI ���̵� ��/�ƿ� ���� �ð�

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
        originalSpeed = nav.speed; // NavMeshAgent�� ���� �ӵ� ����

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
                StartCoroutine(RestoreNavSpeed(4f)); // ���� �ӵ��� �����ϴ� �Լ� 5�ʵ� ȣ��
            }

            // �÷��̾ �߰� ���� ��� �ִϸ��̼��� ���
            animator.SetBool("run", true);
            nav.isStopped = false;

            if (distanceToTarget < getPlayerDistance && !hasPlayedAudio)
            {
                animator.SetBool("atack", true);

                // �÷��̾��� ī�޶� �ٶ󺸵��� ȸ��
                Vector3 targetPosition = new Vector3(nav.transform.position.x, nav.transform.position.y + 3, nav.transform.position.z);
                target.GetComponentInChildren<Camera>().transform.LookAt(targetPosition);
                audioSource.Stop();
                // ����� Ŭ������ ���ÿ� ���
                Debug.Log("���� ���");
                getPlayerAudioSource.PlayOneShot(getPlayerClip);
                playerScreamingAudioSource.PlayOneShot(playerScreamingClip);
                surprisedSoundAudioSource.PlayOneShot(surprisedSoundClip);
                badEndingsongAudioSource.PlayOneShot(badEndingsongClip);
                Debug.Log("���� ���");
                getPlayerAudioSource.PlayOneShot(getPlayerClip);
                playerScreamingAudioSource.PlayOneShot(playerScreamingClip);
                surprisedSoundAudioSource.PlayOneShot(surprisedSoundClip);
                badEndingsongAudioSource.PlayOneShot(badEndingsongClip);

                Debug.Log("5f �ȿ� ����");
                if (uiCoroutine == null)
                {
                    Debug.Log("uiCoroutine == null)���� ����");
                    uiCoroutine = StartCoroutine(ShowUIAfterDelay());
                }

                hasPlayedAudio = true; // ������� ����Ǿ����� ǥ��
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

    IEnumerator RestoreNavSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);

        nav.speed = originalSpeed; // ������ NavMeshAgent �ӵ��� ����
        Debug.Log("�����ӵ� ����");
        isSpeedModified = true;
    }

    IEnumerator ShowUIAfterDelay()
    {
        yield return new WaitForSeconds(uiDelayTime);

        isUIActive = true;

        // UI ��� Ȱ��ȭ
        Debug.Log("ĵ���� Ȱ��ȭ");
        uiCanvas.gameObject.SetActive(true);
        deadImage.SetActive(true);
        retryButton.SetActive(true);

        float t = 0f;
        while (t < uiFadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / uiFadeDuration);

            // UI ����� ���� ���� �����Ͽ� ���̵� �� ȿ�� ����
            Color deadImageColor = deadImage.GetComponent<Image>().color;
            deadImageColor.a = alpha;
            deadImage.GetComponent<Image>().color = deadImageColor;

            Color retryButtonColor = retryButton.GetComponent<Image>().color;
            retryButtonColor.a = alpha;
            retryButton.GetComponent<Image>().color = retryButtonColor;

            yield return null;
        }
    }
}

