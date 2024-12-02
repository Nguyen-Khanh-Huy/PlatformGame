using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : Collectable
{
    protected override void TriggerHandle()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxCollectCoin);
        PlayerManager.Ins.coin++;
        GameData.Ins.SaveGame();
    }
}
