using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHp : Collectable
{
    protected override void TriggerHandle()
    {
        PlayerCtrl.Ins.CurHp++;
        PlayerManager.Ins.hp = PlayerCtrl.Ins.CurHp;
        GameData.Ins.SaveGame();
    }
}
