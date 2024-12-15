using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public PlayerData playerData;
    public AudioData audioData;
    public LevelData levelData;
    public void SaveAllData()
    {
        Pref.GameData = JsonUtility.ToJson(this);
    }
    public void LoadAllData()
    {
        string data = Pref.GameData;
        if (string.IsNullOrEmpty(data)) return;

        JsonUtility.FromJsonOverwrite(data, this);
    }

    //-------------------------------------------------------------------------

    public void SaveGame()
    {
        playerData = PlayerManager.Ins.SaveData();
        levelData = LevelManager.Ins.SaveData();
        audioData = AudioManager.Ins.SaveData();
        SaveAllData();
    }

    public void LoadGame()
    {
        LoadAllData();
        PlayerManager.Ins.LoadData(playerData);
        LevelManager.Ins.LoadData(levelData);
        AudioManager.Ins.LoadData(audioData);
    }
}
