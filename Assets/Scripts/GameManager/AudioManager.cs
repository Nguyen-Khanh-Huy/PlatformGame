using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public float musicVol;
    public float soundVol;

    public void Initialize(AudioData data)
    {
        if (data == null) return;

        musicVol = data.musicVol;
        soundVol = data.soundVol;
    }
    public AudioData GetData()
    {
        return new AudioData(musicVol, soundVol);
    }
}
