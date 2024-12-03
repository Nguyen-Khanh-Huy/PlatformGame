using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlayManager : Singleton<UIGamePlayManager>
{
    public GameObject UIMobileGamepad;
    public GameObject UISettingDialog;
    public GameObject UIPauseDialog;
    public GameObject UILevelPassedDialog;
    public GameObject UILevelFailDialog;

    [SerializeField] private Button _btnPause;

    [SerializeField] private TMP_Text _txtLife;
    [SerializeField] private TMP_Text _txtHp;
    [SerializeField] private TMP_Text _txtCoin;
    [SerializeField] private TMP_Text _txtKey;
    [SerializeField] private TMP_Text _txtTime;
    [SerializeField] private TMP_Text _txtBullet;

    public override void Awake()
    {
        DontDestroy(false);
    }
    private void Start()
    {
        AudioManager.Ins.PlayMusic(AudioManager.Ins.MusicGamePlay);
        LevelManager.Ins.gamePlayTime = 0f;
        LevelManager.Ins.checkPlayTime = true;

        _btnPause.onClick.AddListener(() => Show(UIPauseDialog));
    }
    private void Update()
    {
        _txtLife.text = PlayerManager.Ins.life.ToString();
        _txtHp.text = PlayerManager.Ins.hp.ToString();
        _txtCoin.text = PlayerManager.Ins.coin.ToString();
        _txtKey.text = PlayerManager.Ins.key.ToString();
        _txtTime.text = LevelManager.Ins.TimeConvert(LevelManager.Ins.gamePlayTime);
        _txtBullet.text = PlayerManager.Ins.bullet.ToString();
    }
    public void Show(GameObject obj)
    {
        if (obj == UILevelPassedDialog)
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxLevelPassed);
        }
        else if (obj == UILevelFailDialog)
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxLevelFail);
        }
        else
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        }
        obj.SetActive(true);
        LevelManager.Ins.checkPlayTime = false;
    }
    public void Close(GameObject obj)
    {
        obj.SetActive(false);
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        LevelManager.Ins.checkPlayTime = true;
    }
}
