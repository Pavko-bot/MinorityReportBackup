using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshPro display;

    public float timeValue;

    private bool triggerCountdownSounds = false;
    public bool freezeTimer = true;

    private void Start()
    {
        display.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (freezeTimer)
        {
            StartCoroutine(StartTimerAfterTime(7));
        }
        else
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
                SceneManager.LoadScene("GameOverScene");
            }

            if (!triggerCountdownSounds && timeValue <= 11)
            {
                SoundManager.PlaySound(SoundManager.Sound.Countdown);
                triggerCountdownSounds = true;
            }

        }
            DisplayTime(timeValue);
    }

    void DisplayTime(float currentTime)
    {
        if(currentTime < 0)
        {
            currentTime = 0;
        }

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        display.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator StartTimerAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        freezeTimer = false;
    }
}
