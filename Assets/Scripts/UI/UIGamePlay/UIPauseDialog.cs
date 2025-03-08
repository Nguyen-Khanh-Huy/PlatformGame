using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseDialog : MonoBehaviour
{
    [SerializeField] private Button _btnRestart;
    [SerializeField] private Button _btnSetting;
    [SerializeField] private Button _btnExit;
    [SerializeField] private Button _btnResume;
    private void Start()
    {
        _btnRestart.onClick.AddListener(BtnRestart);
        _btnSetting.onClick.AddListener(BtnSetting);
        _btnExit.onClick.AddListener(BtnExit);
        _btnResume.onClick.AddListener(() => UIGamePlayManager.Ins.Close(gameObject));
    }
    private void BtnRestart()
    {
        UIGamePlayManager.Ins.Close(gameObject);
        SceneController.Ins.LoadLevelScene(LevelManager.Ins.levelId);
    }
    private void BtnSetting()
    {
        UIGamePlayManager.Ins.Close(gameObject);
        UIGamePlayManager.Ins.Show(UIGamePlayManager.Ins.UISettingDialog);
    }
    private void BtnExit()
    {
        UIGamePlayManager.Ins.Close(gameObject);
        SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
    }

}
