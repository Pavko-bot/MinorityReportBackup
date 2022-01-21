using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets _s;

    public static SoundAssets s
    {
        get
        {
            if (_s == null) _s = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
            return _s;
        }
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
