using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMove : MonoBehaviour
{
    public float sensitivity = 250f;
    public float rotationX;
    public float rotationY;

    public GameObject targetObject;
    public float targetDistance = 20f;
    public float returnSpeed = 0.2f;
    public float moveSpeed = 0.1f;

    public AudioClip lookAMonsterClip;
    public AudioClip lookEnterMonster1Clip;
    public AudioClip chaseAfterPlayerClip;
    public AudioClip reliefPlayerSound; // �i�� ������ ��Ÿ���� ����� Ŭ��

    private bool isLookingAtObject = false;
    private bool canRotate = true;
    private bool hasLookedAtObject = false;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    private AudioSource audioSource;
    private bool isPlayingChaseMusic = false;
    private float startVolume;
    private bool isUIActive = false; // UI�� Ȱ��ȭ�Ǿ� �ִ��� ���θ� ��Ÿ���� �÷���
    private Coroutine uiCoroutine; // UI ������ �� ���̵� ��/�ƿ��� �����ϱ� ���� Coroutine ����

    public Canvas uiCanvas; // UI Canvas
    public GameObject deadImage; // ��� �̹���
    public GameObject retryButton; // ����� ��ư
    public float uiDelayTime = 3f; // UI ������ �ð�
    public float uiFadeDuration = 2f; // UI ���̵� ��/�ƿ� ���� �ð�



    void Start()
    {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        targetObject = GameObject.Find("Zombie");

        audioSource = GetComponent<AudioSource>();
       
    }

    void Update()
    {

        if (canRotate)
        {
            float mouseMoveX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            rotationY += mouseMoveX;

            float mouseMoveY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            rotationX -= mouseMoveY;

            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }

        if (!isLookingAtObject && !hasLookedAtObject && Vector3.Distance(transform.position, targetObject.transform.position) <= targetDistance)
        {
            isLookingAtObject = true;
            StartCoroutine(LookAtObject());
            
        }
    }

    IEnumerator LookAtObject()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetObject.transform.position - transform.position);
        float t = 0f;

        canRotate = false;

        audioSource.clip = lookAMonsterClip;
        audioSource.Play();

        AudioSource.PlayClipAtPoint(lookEnterMonster1Clip, transform.position);

        while (t < 1f)
        {
            t += Time.deltaTime * returnSpeed;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            transform.rotation = Quaternion.Euler(newRotation.eulerAngles.x, newRotation.eulerAngles.y, 0f);

            transform.position = Vector3.Lerp(originalPosition, targetObject.transform.position, t * moveSpeed);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        canRotate = true;
        isLookingAtObject = false;
        hasLookedAtObject = true;

        audioSource.Stop();

        transform.position = originalPosition;

        StartCoroutine(PlayChaseAfterPlayerClip());
    }

    IEnumerator PlayChaseAfterPlayerClip()
    {
        yield return new WaitForSeconds(0f);

        if (Vector3.Distance(transform.position, targetObject.transform.position) <= targetDistance)
        {
            isPlayingChaseMusic = true;
            audioSource.clip = chaseAfterPlayerClip;
            audioSource.Play();
        }
    }

    void LateUpdate()
    {
        if (isPlayingChaseMusic && Vector3.Distance(transform.position, targetObject.transform.position) > targetDistance)
        {
            StartCoroutine(FadeOutMusic(4f));
        }
        if (isPlayingChaseMusic && Vector3.Distance(transform.position, targetObject.transform.position) <= 5f)
        {
            StartCoroutine(getmonsterMusic(0f)); // fadeTime�� 0���� �����Ͽ� ��� ������ �����մϴ�.
            if (uiCoroutine == null)
            {
                uiCoroutine = StartCoroutine(ShowUIAfterDelay());
            }
        }
    }

    IEnumerator FadeOutMusic(float fadeTime)
    {
        startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
            yield return null;
        }

        audioSource.Stop();
        isPlayingChaseMusic = false;

        audioSource.volume = startVolume; // ������ ������� �ǵ���

        audioSource.clip = reliefPlayerSound;
        audioSource.Play();
    }

    IEnumerator getmonsterMusic(float fadeTime)
    {
        audioSource.Stop();
        isPlayingChaseMusic = false;
        yield return null;
    }
    IEnumerator ShowUIAfterDelay()
    {
        yield return new WaitForSeconds(uiDelayTime);

        isUIActive = true;

        // UI ��� Ȱ��ȭ
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
