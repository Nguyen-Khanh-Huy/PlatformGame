using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBullet : Collectable
{
    protected override void TriggerHandle()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxCollectBullet);
        PlayerManager.Ins.bullet++;
        GameData.Ins.SaveGame();
    }
}
