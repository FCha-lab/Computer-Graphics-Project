using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shackhead : MonoBehaviour
{
    public Transform cameraTransform; // ��鸲�� ������ ī�޶��� Transform ������Ʈ
    public float shakeDuration; // ��鸲 ���� �ð�
    public float shakeMagnitudef; // ��鸲�� ����

    public float updateInterval = 1f; // ������Ʈ �ֱ� ����

    private float updateTimer = 0f; // ������Ʈ Ÿ�̸�

    private Quaternion originalRotation; // �ʱ� ī�޶� ȸ����

    private float rotationX;
    private float rotationY;

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent(typeof(Transform)) as Transform;
        }
        rotationX = Random.Range(-shakeMagnitudef, shakeMagnitudef) * 0.2f;
        rotationY = Random.Range(-shakeMagnitudef, shakeMagnitudef) * 0.2f;
    }

    void OnEnable()
    {
        originalRotation = cameraTransform.localRotation;
    }

    void Update()
    {
        updateTimer += Time.deltaTime;
        Quaternion randomRotation = cameraTransform.localRotation;
        if (updateTimer >= updateInterval) {
            rotationX = Random.Range(-shakeMagnitudef, shakeMagnitudef) * 0.2f;
            rotationY = Random.Range(-shakeMagnitudef, shakeMagnitudef) * 0.2f;

            updateTimer = 0f;
        }

        randomRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        // �����Ͽ� �ε巯�� ��ȯ ����
        cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, randomRotation, Time.deltaTime / updateInterval);

    }

}

