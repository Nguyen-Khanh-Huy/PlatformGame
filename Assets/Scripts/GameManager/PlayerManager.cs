using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerSO _playerSO;

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
        PlayerCtrl.Ins.CurHp = _playerSO.Hp;
        hp = PlayerCtrl.Ins.CurHp;
        GameData.Ins.SaveGame();
    }
    public void PlayerNotFirst()
    {
        PlayerCtrl.Ins.CurHp = hp;
    }
}
