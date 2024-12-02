using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingDialog : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [SerializeField] private Button _btnSave;
    private void Start()
    {
        _musicSlider.value = AudioManager.Ins.VolumeMusic;
        _sfxSlider.value = AudioManager.Ins.VolumeSFX;

        _musicSlider.onValueChanged.AddListener(VolumeChangeMusic);
        _sfxSlider.onValueChanged.AddListener(VolumeChangeSfx);

        _btnSave.onClick.AddListener(BtnAction);
    }
    private void VolumeChangeMusic(float volume)
    {
        AudioManager.Ins.AusMusic.volume = volume;
    }
    private void VolumeChangeSfx(float volume)
    {
        AudioManager.Ins.AusSFX.volume = volume;
    }
    private void BtnAction()
    {
        AudioManager.Ins.VolumeMusic = _musicSlider.value;
        AudioManager.Ins.VolumeSFX = _sfxSlider.value;
        GameData.Ins.SaveGame();
        UIMenuManager.Ins.Close(gameObject);
    }
}
