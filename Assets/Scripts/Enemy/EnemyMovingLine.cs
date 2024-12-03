using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingLine : Enemy
{
    [SerializeField] private EnemyDir _enemyDir;
    private Vector3 _posXLeft;
    private Vector3 _posXRight;
    private Vector3 _posYUp;
    private Vector3 _posYDown;
    private float _timeCheckTarget = 0f;
    [SerializeField] private bool _noTarger;
    protected override void Start()
    {
        base.Start();
        DirStart();
    }
    protected override void Update()
    {
        if (_noTarger) return;
        _timeCheckTarget += Time.deltaTime;
    }
    protected override void Move()
    {
        if(_target != _player.transform.position && _target != _posXLeft && _target != _posXRight && _target != _posYUp && _target != _posYDown) return;
        if (_target == _player.transform.position)
        {
            ChangeState(EnemyState.Chasing);
            _speedCur = _enemySO.SpeedChasing;
        }
        else
        {
            ChangeState(EnemyState.Moving);
            _speedCur = _speedMove;
            _target = Vector3.Distance(transform.position, _posXLeft) < 0.5f ? _posXRight : Vector3.Distance(transform.position, _posXRight) < 0.5f ? _posXLeft : _target;
            _target = Vector3.Distance(transform.position, _posYUp) < 0.5f ? _posYDown : Vector3.Distance(transform.position, _posYDown) < 0.5f ? _posYUp : _target;
        }
        _direction = _target - transform.position;
        _direction.Normalize();
        if (GetComponent<MovingPlatform>() != null)
        {
            transform.position += _direction * _speedCur * Time.deltaTime;
        }
        else 
        { 
            if(_knockBack) return;
            _rb.velocity = _direction * _speedCur; 
        }

        CheckFlipX(_rb.velocity.x < 0f);
        if (gameObject.name == "Fish_Vertical")
        {
            CheckFlipY(_rb.velocity.y < 0f);
        }
    }
    private void DirStart()
    {
        _posYUp = new Vector3(transform.position.x, _startPosition.y + _movingDist, transform.position.z);
        _posYDown = new Vector3(transform.position.x, _startPosition.y - _movingDist, transform.position.z);
        _posXRight = new Vector3(_startPosition.x + _movingDist, transform.position.y, transform.position.z);
        _posXLeft = new Vector3(_startPosition.x - _movingDist, transform.position.y, transform.position.z);

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_noTarger) return;
        Player _player = collision.collider.GetComponent<Player>();
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2 dirKnockBack = (_player.transform.position - transform.position).normalized;
            _player.TakeDamagePlayer(_enemySO.Damage, dirKnockBack);
            _timeCheckTarget = 0f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_noTarger) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (_timeCheckTarget < 0.5f)
            {
                _target = Vector3.Distance(collision.transform.position, _posXLeft) < Vector3.Distance(transform.position, _posXLeft) ? _posXRight : _posXLeft;
            }
            else { _target = collision.transform.position; }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_noTarger) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (_enemyDir == EnemyDir.Left || _enemyDir == EnemyDir.Right)
            {
                _target = Vector3.Distance(collision.transform.position, _posXLeft) < Vector3.Distance(transform.position, _posXLeft) ? _posXRight : _posXLeft;
            }
            else
            {
                _target = Vector3.Distance(transform.position, _posYUp) < Vector3.Distance(transform.position, _posYDown) ? _posYDown : _posYUp;
            }
        }
    }
}
