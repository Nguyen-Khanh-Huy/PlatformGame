using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableLife : Collectable
{
    protected override void TriggerHandle()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxCollectLife);
        PlayerManager.Ins.life++;
        GameData.Ins.SaveGame();
    }
}
