using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    protected abstract void TriggerHandle();
    private void Start()
    {
        DestroyWhenLevelPassed();
    }
    protected void DestroyWhenLevelPassed()
    {
        //if (GameData.Ins.IsLevelPassed(LevelManager.Ins.CurLevelId))
        //{
        //    Destroy(gameObject);
        //}
    }
    public void Trigger()
    {
        TriggerHandle();
        Destroy(gameObject);
        // vfx
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Trigger();
        }
    }
}
