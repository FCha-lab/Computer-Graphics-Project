using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacherMove : MonoBehaviour
{

    public Transform cameraTransform;
    // Transform���� ī�޶� �����ӿ� ���� �޶����Ƿ�, �ش� ���� ī�޶� �Ѱ��ֱ� ���� // CameraTransform ���� ����
    public CharacterController characterController;
    // CharacterController�� 3D ������Ʈ�� �����ϱ� ���� characterController ���� ����
    public float moveSpeed = 10f;
    // �̵� �ӵ�
    public float jumpSpeed = 10f;
    // ���� �ӵ�
    public float shiftMoveSpeedMultiplier = 2f;
    //shift ���ӵ�
    public float gravity = -20f; // �߷�
    public float yVelocity = 0;
    // Y�� ������

    private bool isShiftPressed = false; // ����Ʈ Ű ���� ����
    public bool isDizzy = false;

    private bool isCameraMoving = false; // ī�޶� �̵� ���� ����
    private float cameraMoveDuration = 0.1f; // ī�޶� �̵� ���� �ð�
    private float cameraMoveTimer = 0f; // ī�޶� �̵� Ÿ�̸�
    private float cameraMoveDistance = 0.1f; // ī�޶� �̵� �Ÿ�
    public AudioClip soundEffect; // ���� ����
    public AudioClip shiftSoundEffect; // �޸��� ���� ����
    private AudioSource audioSource; // ����� �ҽ�
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float currentMoveSpeed = moveSpeed; // ���� �̵� �ӵ� ����

        if (isShiftPressed)
        {
            currentMoveSpeed *= shiftMoveSpeedMultiplier; // ����Ʈ Ű ���� ���¿����� �̵� �ӵ� ��� ����
        }

        Vector3 moveDirection = new Vector3(h, 0, v);
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= currentMoveSpeed;

        if (characterController.isGrounded)
        {
            yVelocity = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }

        yVelocity += (gravity * Time.deltaTime);
        moveDirection.y = yVelocity;
        characterController.Move(moveDirection * Time.deltaTime);

        // Horizontal �� Vertical �Է¿� ���� ī�޶� �̵� ó��
        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {
            if (!isCameraMoving)
            {
                StartCoroutine(MoveCamera());
            }
        }

        // �̵�Ű�� ���� �� ���� ���
        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {
            if (isShiftPressed && shiftSoundEffect != null)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(shiftSoundEffect);
                }
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(soundEffect);
                }
            }
        }

        // ����Ʈ Ű ���� ���� Ȯ��
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isShiftPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isShiftPressed = false;
        }
    }

    IEnumerator MoveCamera()
    {
        isCameraMoving = true;

        Vector3 originalCameraPosition = cameraTransform.localPosition;

        float elapsedTime = 0f;

        if (!isDizzy)
        {

            Vector3 targetCameraPosition = originalCameraPosition + new Vector3(0, cameraMoveDistance, 0);

            // ī�޶� ���� �̵�
            while (elapsedTime < cameraMoveDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / cameraMoveDuration);

                cameraTransform.localPosition = Vector3.Lerp(originalCameraPosition, targetCameraPosition, t);

                yield return null;
            }

            // ī�޶� �Ʒ��� �̵�
            elapsedTime = 0f;

            while (elapsedTime < cameraMoveDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / cameraMoveDuration);

                cameraTransform.localPosition = Vector3.Lerp(targetCameraPosition, originalCameraPosition, t);

                yield return null;
            }
        }

        cameraTransform.localPosition = originalCameraPosition;
        isCameraMoving = false;
    }
}