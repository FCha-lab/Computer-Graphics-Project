using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DrinkPotion : MonoBehaviour
{
    public GameObject target;
    public int drinkedPotion = 0;
    public MouseMove mouse_Mode_class;
    public CharacherMove cm;
    public Camera cam;


    public float blurDuration = 10f; // 블러 지속 시간
    public PostProcessVolume postProcessVolume; // Post-process Volume 컴포넌트

    private AudioSource audioSource;

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
        if(drinkedPotion == 1)  { mouse_Mode_class.spon_1 = true; }
        if (drinkedPotion == 2) { mouse_Mode_class.spon_2 = true; }
        if (drinkedPotion == 3) { mouse_Mode_class.spon_3 = true; }
        if (drinkedPotion == 4) { mouse_Mode_class.spon_4 = true; }

       
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
            Debug.Log("포션 닿아짐");
            drinkedPotion++;
            Destroy(other.gameObject);
            ApplyBlur();
            
        }
    }

    public void ApplyBlur()
    {
        // 블러 효과를 활성화하고 타이머를 초기화합니다.
        postProcessVolume.enabled = true;
        isBlurring = true;
        blurTimer = 0f;
        cm.isDizzy = true;
        mouse_Mode_class.shake = true;
    }

    private void DisableBlur()
    {
        // 블러 효과를 비활성화합니다.
        postProcessVolume.enabled = false;
        isBlurring = false;
        blurTimer = 0f;
        cm.isDizzy = false;
        mouse_Mode_class.shake = false;
        //Vector3 forwardDirection = cam.transform.forward;
       // cam.transform.LookAt(cam.transform.position + forwardDirection);

    }

}
