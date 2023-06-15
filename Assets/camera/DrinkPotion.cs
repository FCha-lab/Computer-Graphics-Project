using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DrinkPotion : MonoBehaviour
{

    public int drinkedPotion = 0;
    public Shackhead s;
    public CharacherMove cm;


    public float blurDuration = 7f; // �� ���� �ð�
    public PostProcessVolume postProcessVolume; // Post-process Volume ������Ʈ

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
        if(drinkedPotion == 4)
        {

        }
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
        s.enabled = true;
    }

    private void DisableBlur()
    {
        // �� ȿ���� ��Ȱ��ȭ�մϴ�.
        postProcessVolume.enabled = false;
        isBlurring = false;
        blurTimer = 0f;
        cm.isDizzy = false;
        s.enabled = false;
    }

}
