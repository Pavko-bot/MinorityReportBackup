using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    void Update()
    {
        if((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false) || Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
