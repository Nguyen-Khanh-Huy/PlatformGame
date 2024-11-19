using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHp : Collectable
{
    protected override void TriggerHandle()
    {
        PlayerManager.Ins.hp++;
        GameData.Ins.SaveGame();
    }
}
