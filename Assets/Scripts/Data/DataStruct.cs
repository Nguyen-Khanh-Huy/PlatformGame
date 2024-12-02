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
public enum GameScene
{
    MainMenu,
    Level_
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
    public float VolumeMusic;
    public float VolumeSFX;
    public AudioData(float VolumeMusic, float VolumeSFX)
    {
        this.VolumeMusic = VolumeMusic;
        this.VolumeSFX = VolumeSFX;
    }
}

[System.Serializable]
public class LevelData
{
    public int levelId;
    public List<Vector3> checkPoints;
    public List<bool> levelUnlockeds;
    public List<bool> levelPasseds;
    public List<float> completeTimes;

    public LevelData(int levelId, List<Vector3> checkPoints, List<bool> levelUnlockeds, List<bool> levelPasseds, List<float> completeTimes)
    {
        this.levelId = levelId;
        this.checkPoints = checkPoints;
        this.levelUnlockeds = levelUnlockeds;
        this.levelPasseds = levelPasseds;
        this.completeTimes = completeTimes;
    }
}

[System.Serializable]
public class Stars
{
    public int timeOneStar;
    public int timeTwoStar;
    public int timeThreeStar;
    public int GetStar(int time)
    {
        if (time <= timeThreeStar) return 3;
        else if (time <= timeTwoStar) return 2;
        else return 1;
    }
}

[System.Serializable]
public class LevelItem
{
    public Sprite imageLevel;
    public Stars starLevel;
}
