using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public float gamePlayTime = 0f;
    public bool checkPlayTime;
    public LevelItem[] levelItems;

    public int levelId;
    public List<Vector3> checkPoints;
    public List<bool> levelUnlockeds;
    public List<bool> levelPasseds;
    public List<float> completeTimes;

    private void Update()
    {
        if (checkPlayTime)
        {
            gamePlayTime += Time.deltaTime;
        }
    }
    public void Initialize(LevelData data)
    {
        if (data == null) return;

        levelId = data.levelId;
        checkPoints = data.checkPoints;
        levelUnlockeds = data.levelUnlockeds;
        levelPasseds = data.levelPasseds;
        completeTimes = data.completeTimes;
    }
    public LevelData GetData()
    {
        return new LevelData(levelId, checkPoints, levelUnlockeds, levelPasseds, completeTimes);
    }
    public string TimeConvert(double time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }
    public void LevelStartFirst()
    {
        if (levelItems == null || levelItems.Length <= 0) return;
        for (int i = 0; i < levelItems.Length; i++)
        {
            if (i == 0)
            {
                levelUnlockeds.Add(true);
            }
            else
            { levelUnlockeds.Add(false); }

            levelId = 0;
            checkPoints.Add(Vector3.zero);
            levelPasseds.Add(false);
            completeTimes.Add(0f);
        }
        GameData.Ins.SaveGame();
    }

    //public void LevelUpdateIndex()
    //{
    //    GameData.Ins.LoadGame();
    //    if (levelItems == null || levelItems.Length <= 0) return;
    //    if (checkPoints.Count < levelItems.Length
    //        || levelUnlockeds.Count < levelItems.Length
    //        || levelPasseds.Count < levelItems.Length
    //        || playTimes.Count < levelItems.Length
    //        || completeTimes.Count < levelItems.Length)
    //    {
    //        for (int i = checkPoints.Count; i < levelItems.Length; i++)
    //        {
    //            checkPoints.Add(Vector3.zero);
    //            levelUnlockeds.Add(false);
    //            levelPasseds.Add(false);
    //            playTimes.Add(0f);
    //            completeTimes.Add(0f);
    //        }
    //      GameData.Ins.SaveGame();
    //    }
    //}
}
