using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.gameOverVoiceline);
        StartCoroutine(ExecuteAfterTime(12));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("IntroScene");
    }
}
