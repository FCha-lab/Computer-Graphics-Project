using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Potion : MonoBehaviour
{

    public float rotationSpeed = 1f;
    public float shakeAmount = 0.1f;
    public float shakeSpeed = 1f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        // 회전과 흔들림에 대한 값을 계산합니다.
        float rotationAmount = Time.deltaTime * rotationSpeed;
        float shakeAmountSin = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        float shakeAmountCos = Mathf.Cos(Time.time * shakeSpeed) * shakeAmount;

        // 회전과 흔들림을 적용합니다.
        transform.rotation = originalRotation * Quaternion.Euler(0f, rotationAmount, 0f);
        transform.position = originalPosition + new Vector3(shakeAmountSin, shakeAmountCos, 0f);
    }


}
