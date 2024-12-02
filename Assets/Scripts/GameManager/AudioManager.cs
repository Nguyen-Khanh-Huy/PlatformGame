using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Range(0, 1)]
    public float VolumeMusic;
    [Range(0, 1)]
    public float VolumeSFX;

    [Header("AudioSource: ")]
    public AudioSource AusMusic;
    public AudioSource AusSFX;

    [Header("AudioClip MUSIC: ")]
    public AudioClip MusicMainMenu;
    public AudioClip MusicGamePlay;

    [Header("AudioClip SFX: ")]
    public AudioClip SfxCollectCoin;
    public AudioClip SfxCollectBullet;
    public AudioClip SfxCollectLife;
    public AudioClip SfxCollectHp;
    public AudioClip SfxCollectKey;

    public AudioClip SfxAttack;
    public AudioClip SfxFireBullet;
    public AudioClip SfxWalk;
    public AudioClip SfxJump;
    public AudioClip SfxLand;
    public AudioClip SfxBuy;
    public AudioClip SfxBuyFail;
    public AudioClip SfxGetHit;
    public AudioClip SfxDeadPlayer;
    public AudioClip SfxDeadEnemy;
    public AudioClip SfxLevelFail;
    public AudioClip SfxLevelPassed;
    public AudioClip SfxBtnClick;

    public void Initialize(AudioData data)
    {
        if (data == null) return;

        VolumeMusic = data.VolumeMusic;
        VolumeSFX = data.VolumeSFX;
    }
    public AudioData GetData()
    {
        return new AudioData(VolumeMusic, VolumeSFX);
    }
    public void AudioStartFirst()
    {
        VolumeMusic = 0.5f;
        VolumeSFX = 1f;
        GameData.Ins.SaveGame();
    }
    public void PlayMusic(AudioClip music)
    {
        AusMusic.clip = music;
        AusMusic.loop = true;
        AusMusic.Play();
    }
    public void PlaySFX(AudioClip sfx)
    {
        AusSFX.clip = sfx;
        AusMusic.loop = false;
        AusSFX.Play();
    }
}
