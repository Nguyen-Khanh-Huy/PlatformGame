using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlayManager : Singleton<UIGamePlayManager>
{
    public GameObject _uiMobileGamepad;
    public GameObject _uiSettingDialog;
    public GameObject _uiPauseDialog;

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
        _btnPause.onClick.AddListener(() => Show(_uiPauseDialog));
    }
    private void Update()
    {
        _txtLife.text = PlayerManager.Ins.life.ToString();
        _txtHp.text = PlayerManager.Ins.hp.ToString();
        _txtCoin.text = PlayerManager.Ins.coin.ToString();
        _txtKey.text = PlayerManager.Ins.key.ToString();
        _txtTime.text = LevelManager.Ins.TimeConvert(LevelManager.Ins.gamePlayTime);
        if (GamePad.Ins.IsOnMobile)
        {
            _txtBullet.text = PlayerManager.Ins.bullet.ToString();
        }
    }
    public void Show(GameObject obj)
    {
        obj.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Close(GameObject obj)
    {
        obj.SetActive(false);
        Time.timeScale = 1f;
    }
}
