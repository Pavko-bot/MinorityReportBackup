using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSounds : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        SoundManager.PlaySound(SoundManager.Sound.Ambience);
        SoundManager.PlaySound(SoundManager.Sound.Sirens);
    }
}
