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
        _btnRestart.onClick.AddListener(PauseRestart);
        _btnSetting.onClick.AddListener(PauseSetting);
        _btnExit.onClick.AddListener(ExitPause);
        _btnResume.onClick.AddListener(() => UIGamePlayManager.Ins.Close(gameObject));
    }
    private void PauseRestart()
    {
        UIGamePlayManager.Ins.Close(gameObject);
        SceneController.Ins.LoadLevelScene(LevelManager.Ins.levelId);
    }
    private void PauseSetting()
    {
        UIGamePlayManager.Ins.Close(gameObject);
        UIGamePlayManager.Ins.Show(UIGamePlayManager.Ins._uiSettingDialog);
    }
    private void ExitPause()
    {
        UIGamePlayManager.Ins.Close(gameObject);
        SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
    }

}
