using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject _vfx;
    protected abstract void TriggerHandle();
    private void Start()
    {
        DestroyWhenLevelPassed();
    }
    protected void DestroyWhenLevelPassed()
    {
        if (LevelManager.Ins.levelPasseds[LevelManager.Ins.levelId])
        {
            Destroy(gameObject);
        }
    }
    protected virtual void Vfx()
    {
        _vfx = Instantiate(_vfx, transform.position, Quaternion.identity);
        Destroy(_vfx, 0.5f);
    }
    public void Trigger()
    {
        TriggerHandle();
        Destroy(gameObject);
        Vfx();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Trigger();
        }
    }
}
