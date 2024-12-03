using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private Vector3 _offset;
    private void Update()
    {
        CheckingOffset();
    }
    private void CheckingOffset()
    {
        if (_sp.flipX == true)
        {
            _offset.x = _offset.x > 0f ? -_offset.x : _offset.x;
        }
        else
        {
            _offset.x = _offset.x < 0f ? -_offset.x : _offset.x;
        }
    }
    private void EventHammerAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + _offset, 1f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D col in colliders)
        {
            if (col != null && !col.isTrigger)
            {
                Enemy _enemy = col.GetComponent<Enemy>();
                if (_enemy != null)
                {
                    Vector2 dirKnockBack = (_enemy.transform.position - transform.position).normalized;
                    _enemy.TakeDamageEnemy(PlayerManager.Ins.PlayerSO.Damage, dirKnockBack);
                }
            }
        }
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxAttack);
    }
    private void EventSfxWalk()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxWalk);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position + _offset, 1f);
    //}
}
