using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int curLevelId;
    public List<Vector3> checkPoints;
    public List<bool> levelUnlockeds;
    public List<bool> levelPasseds;
    public List<float> playTimes;
    public List<float> completeTimes;

    public void Initialize(LevelData data)
    {
        if (data == null) return;

        curLevelId = data.curLevelId;
        checkPoints = data.checkPoints;
        levelUnlockeds = data.levelUnlockeds;
        levelPasseds = data.levelPasseds;
        playTimes = data.playTimes;
        completeTimes = data.completeTimes;
    }
    public LevelData GetData()
    {
        return new LevelData(curLevelId, checkPoints, levelUnlockeds, levelPasseds, playTimes, completeTimes);
    }
}
