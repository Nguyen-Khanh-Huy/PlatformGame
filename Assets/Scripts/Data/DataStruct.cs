using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Jump,
    Attack,
    Bullet,
    Ladder,
    LadderIdle,
    Fly,
    OnAir,
    Land,
    SwimSurface,
    SwimDeep,
    Dead,
    SayHello
}
public enum EnemyState
{
    Moving,
    Chasing
}
public enum EnemyDir
{
    Up,
    Down,
    Left,
    Right
}
public enum GamePref
{
    GameData,
    FirstTime
}

[System.Serializable]
public class PlayerData
{
    public int hp;
    public int life;
    public int bullet;
    public int key;
    public int coin;

    public PlayerData(int hp, int life, int bullet, int key, int coin)
    {
        this.hp = hp;
        this.life = life;
        this.bullet = bullet;
        this.key = key;
        this.coin = coin;
    }
}

[System.Serializable]
public class AudioData
{
    public float musicVol;
    public float soundVol;
    public AudioData(float musicVol, float soundVol)
    {
        this.musicVol = musicVol;
        this.soundVol = soundVol;
    }
}

[System.Serializable]
public class LevelData
{
    public int curLevelId;
    public List<Vector3> checkPoints;
    public List<bool> levelUnlockeds;
    public List<bool> levelPasseds;
    public List<float> playTimes;
    public List<float> completeTimes;

    public LevelData(int curLevelId, List<Vector3> checkPoints, List<bool> levelUnlockeds, List<bool> levelPasseds, List<float> playTimes, List<float> completeTimes)
    {
        this.curLevelId = curLevelId;
        this.checkPoints = checkPoints;
        this.levelUnlockeds = levelUnlockeds;
        this.levelPasseds = levelPasseds;
        this.playTimes = playTimes;
        this.completeTimes = completeTimes;
    }
}
