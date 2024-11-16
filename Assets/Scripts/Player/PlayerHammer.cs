using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammer : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerSO;
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
            if (_offset.x > 0f)
            {
                _offset.x = -_offset.x;
            }
        }
        else
        {
            if (_offset.x < 0f)
            {
                _offset.x = -_offset.x;
            }
        }
    }
    private void HammerAttack()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position + _offset, 0.8f, LayerMask.GetMask("Enemy"));
        if(col != null)
        {
            Enemy _enemy = col.GetComponent<Enemy>();
            Vector2 dir = col.transform.position - transform.position;
            _enemy.TakeDamageEnemy(_playerSO.Damage, dir);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + _offset, 0.8f);
    }
}
