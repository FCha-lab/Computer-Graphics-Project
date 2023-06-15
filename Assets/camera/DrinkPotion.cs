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


    public float blurDuration = 10f; // �� ���� �ð�
    public PostProcessVolume postProcessVolume; // Post-process Volume ������Ʈ

    private AudioSource audioSource;

    private bool isBlurring = false; // �� ���� Ȯ�� ����
    private float blurTimer = 0f; // �� Ÿ�̸�

    private void Start()
    {
        // ���� ���� �� �� ȿ���� �ʱ�ȭ�մϴ�.
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
            // �� ���� ���� Ÿ�̸Ӹ� ������ŵ�ϴ�.
            blurTimer += Time.deltaTime;

            if (blurTimer >= blurDuration)
            {
                // �� �ð��� ������ �� ȿ���� �����մϴ�.
                DisableBlur();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            Debug.Log("���� �����");
            drinkedPotion++;
            Destroy(other.gameObject);
            ApplyBlur();
            
        }
    }

    public void ApplyBlur()
    {
        // �� ȿ���� Ȱ��ȭ�ϰ� Ÿ�̸Ӹ� �ʱ�ȭ�մϴ�.
        postProcessVolume.enabled = true;
        isBlurring = true;
        blurTimer = 0f;
        cm.isDizzy = true;
        mouse_Mode_class.shake = true;
    }

    private void DisableBlur()
    {
        // �� ȿ���� ��Ȱ��ȭ�մϴ�.
        postProcessVolume.enabled = false;
        isBlurring = false;
        blurTimer = 0f;
        cm.isDizzy = false;
        mouse_Mode_class.shake = false;
        //Vector3 forwardDirection = cam.transform.forward;
       // cam.transform.LookAt(cam.transform.position + forwardDirection);

    }

}
