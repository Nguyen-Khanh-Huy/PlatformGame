using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : Collectable
{
    protected override void TriggerHandle()
    {
        PlayerManager.Ins.key++;
        GameData.Ins.SaveGame();
    }
}
