using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacherMove : MonoBehaviour
{

    public Transform cameraTransform;
    // Transform값은 카메라 움직임에 따라 달라지므로, 해당 값을 카메라에 넘겨주기 위한 // CameraTransform 변수 선언
    public CharacterController characterController;
    // CharacterController에 3D 오브젝트를 적용하기 위한 characterController 변수 선언
    public float moveSpeed = 10f;
    // 이동 속도
    public float jumpSpeed = 10f;
    // 점프 속도
    public float shiftMoveSpeedMultiplier = 2f;
    //shift 가속도
    public float gravity = -20f; // 중력
    public float yVelocity = 0;
    // Y축 움직임

    private bool isShiftPressed = false; // 쉬프트 키 누름 여부
    public bool isDizzy = false;

    private bool isCameraMoving = false; // 카메라 이동 상태 변수
    private float cameraMoveDuration = 0.1f; // 카메라 이동 지속 시간
    private float cameraMoveTimer = 0f; // 카메라 이동 타이머
    private float cameraMoveDistance = 0.1f; // 카메라 이동 거리
    public AudioClip soundEffect; // 음악 파일
    public AudioClip shiftSoundEffect; // 달리기 음악 파일
    private AudioSource audioSource; // 오디오 소스
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float currentMoveSpeed = moveSpeed; // 현재 이동 속도 설정

        if (isShiftPressed)
        {
            currentMoveSpeed *= shiftMoveSpeedMultiplier; // 쉬프트 키 누른 상태에서는 이동 속도 배수 적용
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

        // Horizontal 및 Vertical 입력에 따른 카메라 이동 처리
        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {
            if (!isCameraMoving)
            {
                StartCoroutine(MoveCamera());
            }
        }

        // 이동키를 누를 때 음악 재생
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

        // 쉬프트 키 누름 여부 확인
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

            // 카메라를 위로 이동
            while (elapsedTime < cameraMoveDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / cameraMoveDuration);

                cameraTransform.localPosition = Vector3.Lerp(originalCameraPosition, targetCameraPosition, t);

                yield return null;
            }

            // 카메라를 아래로 이동
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