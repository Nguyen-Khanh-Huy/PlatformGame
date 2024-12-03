using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _timeDestroy;
    [HideInInspector] public float DirBullet;
    private void Update()
    {
        transform.position += transform.right * DirBullet * _speedBullet * Time.deltaTime;
        Destroy(gameObject, _timeDestroy);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy _enemy = collision.collider.GetComponent<Enemy>();
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Vector2 dirKnockBack = (collision.transform.position - transform.position).normalized;
            _enemy.TakeDamageEnemy(_playerSO.Damage, dirKnockBack);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
