using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerSO PlayerSO;

    public int hp;
    public int life;
    public int bullet;
    public int key;
    public int coin;

    public void Initialize(PlayerData data)
    {
        if (data == null) return;

        hp = data.hp;
        life = data.life;
        bullet = data.bullet;
        key = data.key;
        coin = data.coin;
    }
    public PlayerData GetData()
    {
        return new PlayerData(hp, life, bullet, key, coin);
    }
    public void PlayerStartFirst()
    {
        hp = PlayerSO.Hp;
        GameData.Ins.SaveGame();
    }
    public void Revival()
    {
        PlayerCtrl.Ins.CurHp = hp;
        hp = PlayerCtrl.Ins.CurHp;
        LevelManager.Ins.BackToCheckPoint();
    }
}
