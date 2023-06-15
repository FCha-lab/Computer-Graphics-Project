using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DrinkPotion : MonoBehaviour
{

    public int drinkedPotion = 0;
    public Shackhead s;
    public CharacherMove cm;
    public Camera cam;


    public float blurDuration = 7f; // 블러 지속 시간
    public PostProcessVolume postProcessVolume; // Post-process Volume 컴포넌트
    public AudioSource drinkSound, drinkBGM;

    private bool isBlurring = false; // 블러 상태 확인 변수
    private float blurTimer = 0f; // 블러 타이머

    private void Start()
    {
        // 게임 시작 시 블러 효과를 초기화합니다.
        DisableBlur();
        postProcessVolume.enabled = true;
    }

    private void Update()
    {
        if(drinkedPotion == 4)
        {

        }
        if (isBlurring)
        {
            // 블러 중인 동안 타이머를 증가시킵니다.
            blurTimer += Time.deltaTime;

            if (blurTimer >= blurDuration)
            {
                // 블러 시간이 지나면 블러 효과를 해제합니다.
                DisableBlur();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            drinkedPotion++;
            Destroy(other.gameObject);
            ApplyBlur();
            drinkSound.enabled = true;
        }
    }

    public void ApplyBlur()
    {
        // 블러 효과를 활성화하고 타이머를 초기화합니다.
        postProcessVolume.enabled = true;
        isBlurring = true;
        blurTimer = 0f;
        cm.isDizzy = true;
        s.enabled = true;
        drinkSound.Play();
        drinkBGM.Play();
    }

    private void DisableBlur()
    {
        // 블러 효과를 비활성화합니다.
        postProcessVolume.enabled = false;
        isBlurring = false;
        blurTimer = 0f;
        cm.isDizzy = false;
        s.enabled = false;
        drinkBGM.Stop();
        //Vector3 forwardDirection = cam.transform.forward;
       // cam.transform.LookAt(cam.transform.position + forwardDirection);

    }

}
