using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : Collectable
{
    protected override void TriggerHandle()
    {
        PlayerManager.Ins.coin++;
        GameData.Ins.SaveGame();
    }
}
