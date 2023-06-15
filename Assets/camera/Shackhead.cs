using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shackhead : MonoBehaviour
{
    public Transform cameraTransform; // 흔들림을 적용할 카메라의 Transform 컴포넌트
    public float shakeDuration; // 흔들림 지속 시간
    public float shakeMagnitudef; // 흔들림의 강도

    public float updateInterval = 1f; // 업데이트 주기 설정

    private float updateTimer = 0f; // 업데이트 타이머

    private Quaternion originalRotation; // 초기 카메라 회전값

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
        // 보간하여 부드러운 전환 수행
        cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, randomRotation, Time.deltaTime / updateInterval);

    }

}

