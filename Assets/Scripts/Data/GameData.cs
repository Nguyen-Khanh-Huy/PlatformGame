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
        playerData = PlayerManager.Ins.GetData();
        levelData = LevelManager.Ins.GetData();
        audioData = AudioManager.Ins.GetData();
        SaveAllData();
    }

    public void LoadGame()
    {
        LoadAllData();
        PlayerManager.Ins.Initialize(playerData);
        LevelManager.Ins.Initialize(levelData);
        AudioManager.Ins.Initialize(audioData);
    }
}
