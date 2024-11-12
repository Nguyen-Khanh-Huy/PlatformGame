using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingLine : Enemy
{
    [SerializeField] private EnemyDir _enemyDir;
    [SerializeField] private Vector3 _posXLeft;
    [SerializeField] private Vector3 _posXRight;
    [SerializeField] private Vector3 _posYUp;
    [SerializeField] private Vector3 _posYDown;
    [SerializeField] private bool _noTarger;
    protected override void Start()
    {
        base.Start();
        DirStart();
    }
    protected override void Move()
    {
        if (_target == _player.transform.position)
        {
            ChangeState(EnemyState.Chasing);
            _speedCur = _enemySO.SpeedChasing;
        }
        else
        {
            ChangeState(EnemyState.Moving);
            _speedCur = _enemySO.SpeedMove;
            _target = Vector3.Distance(transform.position, _posXLeft) < 0.5f ? _posXRight : Vector3.Distance(transform.position, _posXRight) < 0.5f ? _posXLeft : _target;
            _target = Vector3.Distance(transform.position, _posYUp) < 0.5f ? _posYDown : Vector3.Distance(transform.position, _posYDown) < 0.5f ? _posYUp : _target;
        }
        _direction = _target - transform.position;
        _direction.Normalize();
        _rb.velocity = _direction * _speedCur;

        if (_rb.velocity.x > 0f) { CheckFlip(false); }
        else { CheckFlip(true); }
    }
    private void DirStart()
    {
        _posYUp = new Vector3(transform.position.x, _startPosition.y + _enemySO.MovingDist, transform.position.z);
        _posYDown = new Vector3(transform.position.x, _startPosition.y - _enemySO.MovingDist, transform.position.z);
        _posXRight = new Vector3(_startPosition.x + _enemySO.MovingDist, transform.position.y, transform.position.z);
        _posXLeft = new Vector3(_startPosition.x - _enemySO.MovingDist, transform.position.y, transform.position.z);

        switch (_enemyDir)
        {
            case EnemyDir.Up:
                _target = _posYUp;
                break;
            case EnemyDir.Down:
                _target = _posYDown;
                break;
            case EnemyDir.Right:
                _target = _posXRight;
                break;
            case EnemyDir.Left:
                _target = _posXLeft;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_noTarger) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = collision.transform.position;
            if (Vector3.Distance(transform.position, collision.transform.position) <= 1f)
            {
                Debug.Log("zz");
                if (_rb.velocity.x < 0f)
                {
                    _rb.AddForce(Vector3.right * 10f, ForceMode2D.Impulse);
                }
                else
                {
                    _rb.AddForce(Vector3.left * -10f, ForceMode2D.Impulse);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_noTarger) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = collision.transform.position;
            if (Vector3.Distance(transform.position, collision.transform.position) <= 1f)
            {
                Debug.Log("zz");
                if (_rb.velocity.x < 0f)
                {
                    _rb.AddForce(Vector3.right * 10f, ForceMode2D.Impulse);
                }
                else
                {
                    _rb.AddForce(Vector3.left * -10f, ForceMode2D.Impulse);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_noTarger) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (_enemyDir == EnemyDir.Left || _enemyDir == EnemyDir.Right)
            {
                if (Vector3.Distance(transform.position, _posXRight) > Vector3.Distance(transform.position, _posXLeft))
                {
                    _target = _posXLeft;
                }
                else { _target = _posXRight; }
            }
            else
            {
                if (Vector3.Distance(transform.position, _posYUp) > Vector3.Distance(transform.position, _posYDown))
                {
                    _target = _posYDown;
                }
                else { _target = _posYUp; }
            }
        }
    }
}
