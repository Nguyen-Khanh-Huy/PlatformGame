using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBullet : Collectable
{
    protected override void TriggerHandle()
    {
        PlayerManager.Ins.bullet++;
        GameData.Ins.SaveGame();
    }
}
