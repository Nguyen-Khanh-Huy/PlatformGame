using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : Singleton<UIMenuManager>
{
    public GameObject UILevelDialog;
    public GameObject UIShopDialog;
    public GameObject UISettingDialog;

    [SerializeField] private Toggle _toggleMobile;
    [SerializeField] private Button _btnPlayGame;
    [SerializeField] private Button _btnLevel;
    [SerializeField] private Button _btnShop;
    [SerializeField] private Button _btnSetting;
    public override void Awake()
    {
        DontDestroy(false);
    }
    private void Start()
    {
        AudioManager.Ins.PlayMusic(AudioManager.Ins.MusicMainMenu);
        LevelManager.Ins.checkPlayTime = false;
        LevelManager.Ins.gamePlayTime = 0f;

        _toggleMobile.onValueChanged.AddListener(UpdateToggle);
        _btnPlayGame.onClick.AddListener(() => Show(UILevelDialog));
        _btnLevel.onClick.AddListener(() => Show(UILevelDialog));
        _btnShop.onClick.AddListener(() => Show(UIShopDialog));
        _btnSetting.onClick.AddListener(() => Show(UISettingDialog));
    }
    public void Show(GameObject obj)
    {
        obj.SetActive(true);
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
    }
    public void Close(GameObject obj)
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        obj.SetActive(false);
    }
    private void UpdateToggle(bool isOn)
    {
        GamePad.Ins.IsOnMobile = isOn;
    }
}
