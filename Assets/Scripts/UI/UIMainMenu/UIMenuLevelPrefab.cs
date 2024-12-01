using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuLevelPrefab : MonoBehaviour
{
    public Button BtnLevelPb;
    public Image ImgLevelPb;
    public GameObject CheckLockPb;
    public GameObject CheckPassedPb;

    public void SetLevelItem(UIMenuLevelPrefab levelItem, int levelIdx)
    {
        levelItem.ImgLevelPb.sprite = LevelManager.Ins.levelItems[levelIdx].imageLevel;
        levelItem.CheckLockPb.SetActive(!LevelManager.Ins.levelUnlockeds[levelIdx]);
        levelItem.CheckPassedPb.SetActive(LevelManager.Ins.levelPasseds[levelIdx]);
        levelItem.BtnLevelPb.onClick.AddListener(() => ButtonAction(levelIdx));
    }
    private void ButtonAction(int idx)
    {
        if (!LevelManager.Ins.levelUnlockeds[idx]) return;
        LevelManager.Ins.levelId = idx;
        GameData.Ins.SaveGame();
        SceneController.Ins.LoadLevelScene(LevelManager.Ins.levelId);
    }
}
