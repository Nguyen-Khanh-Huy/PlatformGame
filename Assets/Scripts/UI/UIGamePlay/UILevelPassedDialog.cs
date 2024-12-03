using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelPassedDialog : MonoBehaviour
{
    [SerializeField] private Image[] _star;
    [SerializeField] private Sprite _activeStar;
    [SerializeField] private Sprite _unactiveStar;

    [SerializeField] private Button _btnReplay;
    [SerializeField] private Button _btnNextLevel;

    [SerializeField] private TMP_Text _txtLife;
    [SerializeField] private TMP_Text _txtHp;
    [SerializeField] private TMP_Text _txtTime;
    [SerializeField] private TMP_Text _txtCoin;
    private void Start()
    {
        ShowStar();
        _btnReplay.onClick.AddListener(BtnReplay);
        _btnNextLevel.onClick.AddListener(BtnNextLevel);
    }
    private void Update()
    {
        _txtLife.text = PlayerManager.Ins.life.ToString();
        _txtHp.text = PlayerManager.Ins.hp.ToString();
        _txtTime.text = LevelManager.Ins.TimeConvert(LevelManager.Ins.gamePlayTime).ToString();
        _txtCoin.text = PlayerManager.Ins.coin.ToString();
    }
    private void ShowStar()
    {
        if (_star != null && _star.Length > 0)
        {
            for (int i = 0; i < LevelManager.Ins.GetStar(); i++)
            {
                var star = _star[i];
                if (star != null)
                {
                    star.sprite = _activeStar;
                }
            }
            if (LevelManager.Ins.GetStar() == _star.Length) return;
            for (int j = LevelManager.Ins.GetStar(); j < _star.Length; j++)
            {
                var star = _star[j];
                if (star != null)
                {
                    star.sprite = _unactiveStar;
                }
            }
        }
    }
    private void BtnReplay()
    {
        SceneController.Ins.LoadLevelScene(LevelManager.Ins.levelId);
    }
    private void BtnNextLevel()
    {
        LevelManager.Ins.levelId++;
        if (LevelManager.Ins.levelId >= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneController.Ins.LoadScene(GameScene.MainMenu.ToString());
        }
        else
        {
            SceneController.Ins.LoadLevelScene(LevelManager.Ins.levelId);
        }
    }
}
