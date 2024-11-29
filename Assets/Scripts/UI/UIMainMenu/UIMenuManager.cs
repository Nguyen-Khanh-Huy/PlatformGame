using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : Singleton<UIMenuManager>
{
    [SerializeField] private Button _btnPlayGame;
    [SerializeField] private Button _btnLevel;
    [SerializeField] private Button _btnShop;
    [SerializeField] private Button _btnSetting;
    public GameObject _uiLevelDialog;
    public GameObject _uiShopDialog;
    public GameObject _uiSettingDialog;
    public override void Awake()
    {
        DontDestroy(false);
    }
    private void Start()
    {
        _btnPlayGame.onClick.AddListener(() => Show(_uiLevelDialog));
        _btnLevel.onClick.AddListener(() => Show(_uiLevelDialog));
        _btnShop.onClick.AddListener(() => Show(_uiShopDialog));
        _btnSetting.onClick.AddListener(() => Show(_uiSettingDialog));
    }
    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void Close(GameObject obj)
    {
        obj.SetActive(false);
    }
}
