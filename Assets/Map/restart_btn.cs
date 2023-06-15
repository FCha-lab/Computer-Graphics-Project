using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart_btn : MonoBehaviour
{
    public Canvas uiCanvas; // UI Canvas
    public GameObject deadImage; // 사망 이미지
    public GameObject retryButton; // 재시작 버튼
    public void OnClick()
    {
        Debug.Log("Button Clicked");
        RestartScene();
    }

    private void RestartScene()
    {
        // UI 요소 활성화
        uiCanvas.gameObject.SetActive(false);
        deadImage.SetActive(false);
        retryButton.SetActive(false);
        // 현재 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
