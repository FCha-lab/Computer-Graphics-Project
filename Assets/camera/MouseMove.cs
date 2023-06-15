using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sensitivity = 250f; // 마우스 민감도
    public float rotationX;
    public float rotationY;

    void Update()
    {
        float mouseMoveX = Input.GetAxis("Mouse X"); // 마우스 X축 움직임값을 받아서 mouseMoveX 변수에 저장
        float mouseMoveY = Input.GetAxis("Mouse Y"); // 마우스 Y축 움직임값을 받아서 mouseMoveY 변수에 저장
        rotationY += mouseMoveX * sensitivity * Time.deltaTime; // rotationY 변수의 값은 rotationY + (mouseMoveX * 마우스 민감도 * Time.deltaTime)
        rotationX += mouseMoveY * sensitivity * Time.deltaTime; // rotationX 변수의 값은 rotationX + (mouseMoveY * 마우스 민감도 * Time.deltaTime)

        if (rotationX < -30f)
        {
            rotationX = -30f;
        }
        if (rotationX > 35f)
        {
            rotationX = 35f;
        }

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0); // rotationX값, rotationY값, Z는 0값을 Vector3 형식으로 변환하고, transform 오일러각에 저장함.
    }
}
