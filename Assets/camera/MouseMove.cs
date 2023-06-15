using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sensitivity = 250f; // ���콺 �ΰ���
    public float rotationX;
    public float rotationY;

    void Update()
    {
        float mouseMoveX = Input.GetAxis("Mouse X"); // ���콺 X�� �����Ӱ��� �޾Ƽ� mouseMoveX ������ ����
        float mouseMoveY = Input.GetAxis("Mouse Y"); // ���콺 Y�� �����Ӱ��� �޾Ƽ� mouseMoveY ������ ����
        rotationY += mouseMoveX * sensitivity * Time.deltaTime; // rotationY ������ ���� rotationY + (mouseMoveX * ���콺 �ΰ��� * Time.deltaTime)
        rotationX += mouseMoveY * sensitivity * Time.deltaTime; // rotationX ������ ���� rotationX + (mouseMoveY * ���콺 �ΰ��� * Time.deltaTime)

        if (rotationX < -30f)
        {
            rotationX = -30f;
        }
        if (rotationX > 35f)
        {
            rotationX = 35f;
        }

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0); // rotationX��, rotationY��, Z�� 0���� Vector3 �������� ��ȯ�ϰ�, transform ���Ϸ����� ������.
    }
}
