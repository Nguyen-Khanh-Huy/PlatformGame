using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootBullet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _offset;
    [SerializeField] private float _curDirBullet;
    private void Update()
    {
        CheckFirePoint();
    }
    private void CheckFirePoint()
    {
        _curDirBullet = _sp.flipX ? -1 : 1;
        _offset = _sp.flipX ? -1.3f : 0f;
    }
    private void ShootBulletPrb()
    {
        var bulletClone = Instantiate(_bulletPrefab, new Vector2(_firePoint.position.x + _offset, _firePoint.position.y), Quaternion.identity);
        bulletClone.DirBullet = _curDirBullet;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(new Vector2(FirePoint.position.x + _offset, FirePoint.position.y), 0.1f);
    //}
}
