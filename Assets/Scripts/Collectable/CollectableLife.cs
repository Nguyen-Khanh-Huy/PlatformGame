using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableLife : Collectable
{
    protected override void TriggerHandle()
    {
        PlayerManager.Ins.life++;
        GameData.Ins.SaveGame();
    }
}
