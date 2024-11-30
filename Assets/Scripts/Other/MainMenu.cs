using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (Pref.FirstTime)
        {
            Debug.Log("Lan 1");
            GameData.Ins.SaveGame();
            LevelManager.Ins.LevelStartFirst();
            PlayerManager.Ins.PlayerStartFirst();
        }
        else
        {
            Debug.Log("Lan 2,3,4,5,...");
            //LevelManager.Ins.LevelUpdateIndex();
            GameData.Ins.LoadGame();
            //LevelManager.Ins.levelPasseds[LevelManager.Ins.levelId] = false;
        }
        Pref.FirstTime = false;
    }
}
