using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart_btn : MonoBehaviour
{
    public Canvas uiCanvas; // UI Canvas
    public GameObject deadImage; // ��� �̹���
    public GameObject retryButton; // ����� ��ư
    public void OnClick()
    {
        Debug.Log("Button Clicked");
        RestartScene();
    }

    private void RestartScene()
    {
        // UI ��� Ȱ��ȭ
        uiCanvas.gameObject.SetActive(false);
        deadImage.SetActive(false);
        retryButton.SetActive(false);
        // ���� ���� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
