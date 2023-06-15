using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMove : MonoBehaviour
{
    public float sensitivity = 250f;
    public float rotationX;
    public float rotationY;


    public GameObject maintarget;
    public GameObject targetObject_1;
    public GameObject targetObject_2;
    public GameObject targetObject_3;
    public GameObject targetObject_4;
    public float targetDistance = 20f;
    public float returnSpeed = 0.2f;
    public float moveSpeed = 0.1f;

    public AudioClip lookAMonsterClip;
    public AudioClip lookEnterMonster1Clip;
    public AudioClip chaseAfterPlayerClip;
    public AudioClip reliefPlayerSound; // 쫒음 해제를 나타내는 오디오 클립

    private bool isLookingAtObject = false;
    private bool canRotate = true;
    private bool hasLookedAtObject = false;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    private AudioSource audioSource;
    private bool isPlayingChaseMusic = false;
    private float startVolume;


    public Transform cameraTransform; // 흔들림을 적용할 카메라의 Transform 컴포넌트
    public float shakeDuration; // 흔들림 지속 시간
    public float shakeMagnitudef; // 흔들림의 강도
    public AudioClip drinkigPotion; // 음악 파일
    public AudioClip afterdrinkigPotion; //음악 파일


    public bool spon_1 = false;
    public bool spon_2 = false;
    public bool spon_3 = false;
    public bool spon_4 = false;


    public bool shake = false;
    




    void Start()
    {
        
        //targetObject_1 = GameObject.Find("Zombie");
        //targetObject_2 = GameObject.Find("Horror_Mutant(1)");
        //targetObject_3 = GameObject.Find("monster_scavenger_Skin_Red");
        //targetObject_4 = GameObject.Find("thc6");

        audioSource = GetComponent<AudioSource>();
        startVolume = audioSource.volume;
    }

  


    void Update()
    {
        Awake();
        if (spon_1 && targetObject_1 != null) { StartCoroutine(ActivateObjectAfterDelay(targetObject_1, 15f)); }
        if (spon_2 && targetObject_2 != null) { StartCoroutine(ActivateObjectAfterDelay(targetObject_2, 15f)); }
        if (spon_3 && targetObject_3 != null) { StartCoroutine(ActivateObjectAfterDelay(targetObject_3, 15f)); }
        if (spon_4 && targetObject_4 != null) { StartCoroutine(ActivateObjectAfterDelay(targetObject_4, 15f)); }

        if (canRotate)
        {
            float mouseMoveX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            rotationY += mouseMoveX;

            float mouseMoveY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            rotationX -= mouseMoveY;

            rotationX = Mathf.Clamp(rotationX, -50f, 50f);

            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }

        //if (!isLookingAtObject && !hasLookedAtObject)
        //{
        //    originalRotation = transform.rotation;
        //    originalPosition = transform.position;
        //    if (spon_1 && Vector3.Distance(transform.position, targetObject_1.transform.position) <= targetDistance)
        //    {
        //        isLookingAtObject = true;
        //        maintarget = targetObject_1;
        //        StartCoroutine(LookAtObject());
        //    }
        //    else if (spon_2 && Vector3.Distance(transform.position, targetObject_2.transform.position) <= targetDistance)
        //    {
        //        isLookingAtObject = true;
        //        maintarget = targetObject_2;
        //        StartCoroutine(LookAtObject());
        //    }
        //    else if (spon_3 && Vector3.Distance(transform.position, targetObject_3.transform.position) <= targetDistance)
        //    {
        //        isLookingAtObject = true;
        //        maintarget = targetObject_3;
        //        StartCoroutine(LookAtObject());
        //    }
        //    else if (spon_4 && Vector3.Distance(transform.position, targetObject_4.transform.position) <= targetDistance)
        //    {
        //        isLookingAtObject = true;
        //        maintarget = targetObject_4;
        //        StartCoroutine(LookAtObject());
        //    }
        //}

    }

    IEnumerator ActivateObjectAfterDelay(GameObject targetObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetObject.SetActive(true);
        if (targetObject == targetObject_1)
        {
            spon_1 = true;
            yield return new WaitForSeconds(1f); // Wait for an additional 15 seconds before looking at the object
            if (!isLookingAtObject && !hasLookedAtObject && spon_1 && Vector3.Distance(transform.position, targetObject_1.transform.position) <= targetDistance)
            {   originalRotation = transform.rotation;
                originalPosition = transform.position;
                audioSource.Stop();
                audioSource.volume = startVolume;
                isLookingAtObject = true;
                maintarget = targetObject_1;
                StartCoroutine(LookAtObject());
            }
        }
        else if (targetObject == targetObject_2)
        {
            spon_2 = true;
            yield return new WaitForSeconds(1f);
            if (!isLookingAtObject && !hasLookedAtObject && spon_2 && Vector3.Distance(transform.position, targetObject_2.transform.position) <= targetDistance)
            {
                originalRotation = transform.rotation;
                originalPosition = transform.position;
                audioSource.Stop();
                audioSource.volume = startVolume;
                isLookingAtObject = true;
                maintarget = targetObject_2;
                StartCoroutine(LookAtObject());
            }
        }
        else if (targetObject == targetObject_3)
        {
            spon_3 = true;
            yield return new WaitForSeconds(1f);
            if (!isLookingAtObject && !hasLookedAtObject && spon_3 && Vector3.Distance(transform.position, targetObject_3.transform.position) <= targetDistance)
            {
                originalRotation = transform.rotation;
                originalPosition = transform.position;
                audioSource.Stop();
                audioSource.volume = startVolume;
                isLookingAtObject = true;
                maintarget = targetObject_3;
                StartCoroutine(LookAtObject());
            }
        }
        else if (targetObject == targetObject_4)
        {
            spon_4 = true;
            yield return new WaitForSeconds(1f);
            if (!isLookingAtObject && !hasLookedAtObject && spon_4 && Vector3.Distance(transform.position, targetObject_4.transform.position) <= targetDistance)
            {
                originalRotation = transform.rotation;
                originalPosition = transform.position;
                audioSource.Stop();
                audioSource.volume = startVolume;
                isLookingAtObject = true;
                maintarget = targetObject_4;
                StartCoroutine(LookAtObject());
            }
        }
    }


    IEnumerator LookAtObject()
    {
        Quaternion targetRotation = Quaternion.LookRotation(maintarget.transform.position - transform.position);
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

            transform.position = Vector3.Lerp(originalPosition, maintarget.transform.position, t * moveSpeed);

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

        if (Vector3.Distance(transform.position, maintarget.transform.position) <= targetDistance)
        {
            isPlayingChaseMusic = true;
            audioSource.clip = chaseAfterPlayerClip;
            audioSource.Play();
            
        }
    }

    void LateUpdate()
    {
        if (isPlayingChaseMusic && Vector3.Distance(transform.position, maintarget.transform.position) > targetDistance)
        {
            StartCoroutine(FadeOutMusic(4f));
        }
        if (isPlayingChaseMusic && Vector3.Distance(transform.position, maintarget.transform.position) <= 5f)
        {
            StartCoroutine(getmonsterMusic(0f)); // fadeTime을 0으로 설정하여 즉시 음악을 중지합니다.

            
        }
    }

    IEnumerator FadeOutMusic(float fadeTime)
    {
        startVolume = audioSource.volume;
        float t = 0f;
        canRotate = true;
        isLookingAtObject = false;
        hasLookedAtObject = false;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
            yield return null;
        }

        audioSource.Stop();
        isPlayingChaseMusic = false;
        

        audioSource.volume = startVolume; // 볼륨을 원래대로 되돌림

        audioSource.clip = reliefPlayerSound;
        audioSource.Play();
    }

    IEnumerator getmonsterMusic(float fadeTime)
    {
        audioSource.Stop();
        isPlayingChaseMusic = false;
        yield return null;
    }
   
    private bool hasPlayedPotionAudio = false;
    private Coroutine resetPotionAudioCoroutine;
    void Awake()
    {
        if (shake)
        {
            if (!hasPlayedPotionAudio)
            {
                audioSource.PlayOneShot(drinkigPotion);
                audioSource.PlayOneShot(afterdrinkigPotion);
                hasPlayedPotionAudio = true;

                if (resetPotionAudioCoroutine != null)
                    StopCoroutine(resetPotionAudioCoroutine);

                resetPotionAudioCoroutine = StartCoroutine(ResetPotionAudioAfterDelay(10f));
            }
            StartCoroutine(ShakeCamera());
        }
    }
    IEnumerator ResetPotionAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasPlayedPotionAudio = false;
    }
    IEnumerator ShakeCamera()
    {
        yield return new WaitForSeconds(2.2f);
        Debug.Log("shake에 true값 적용됨");
       
        
            cameraTransform = GetComponent(typeof(Transform)) as Transform;
        
        rotationX = Random.Range(-shakeMagnitudef, shakeMagnitudef) * 0.5f;
        rotationY = Random.Range(-shakeMagnitudef, shakeMagnitudef) * 0.5f;
    }
}
