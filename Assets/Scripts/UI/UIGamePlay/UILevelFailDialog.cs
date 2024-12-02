using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelFailDialog : MonoBehaviour
{
    [SerializeField] private Button _btnReplay;
    [SerializeField] private Button _btnBackMainMenu;

    [SerializeField] private TMP_Text _txtTime;
    [SerializeField] private TMP_Text _txtKey;
    [SerializeField] private TMP_Text _txtCoin;

    private void Start()
    {
        _btnReplay.onClick.AddListener(BtnReplay);
        _btnBackMainMenu.onClick.AddListener(BtnBackMainMenu);
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _txtTime.text = LevelManager.Ins.TimeConvert(LevelManager.Ins.gamePlayTime);
            _txtKey.text = PlayerManager.Ins.key.ToString();
            _txtCoin.text = PlayerManager.Ins.coin.ToString();
        }
    }
    private void BtnReplay()
    {
        LevelManager.Ins.LevelFail();
        SceneController.Ins.LoadLevelScene(LevelManager.Ins.levelId);
    }
    private void BtnBackMainMenu()
    {
        LevelManager.Ins.LevelFail();
        SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
    }
}
