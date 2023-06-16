using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

public class table_ending : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Transform thc6; // Reference to the thc6 object
    public float distanceThreshold = 10f; // Distance threshold to trigger the ending
    public Canvas canvas; // Reference to the Canvas object
    public VideoClip project_End; // Reference to the VideoClip object for the ending
    public VideoPlayer video;

    private bool endingTriggered = false;
    NavMeshAgent nav;
    GameObject target;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < distanceThreshold)
        {
            if (!endingTriggered)
            {
                // Stop any currently playing audio
                AudioSource playerAudioSource = player.GetComponent<AudioSource>();
                if (playerAudioSource != null && playerAudioSource.isPlaying)
                {
                    playerAudioSource.Stop();
                }
                Debug.Log("거리로 들어온거 인식 완료");
                // Set the velocity of the player and thc6 to zero
                NavMeshAgent thc6Rigidbody = thc6.GetComponent<NavMeshAgent>();
                thc6Rigidbody.speed = 0;

                // Play the video using a VideoPlayer component
                
                if (video != null)
                {
                    Debug.Log("비디오 플레이어 재생");
                    video.clip = project_End;
                    video.Play();
                }


                // Activate the canvas
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(true);
                }

                endingTriggered = true; // Set the flag to prevent repeated triggering
            }
        }
    }
}
