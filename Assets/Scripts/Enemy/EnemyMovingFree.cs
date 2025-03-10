using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovingFree : Enemy
{
    [SerializeField] private bool _canRotation;

    private bool _newPos;
    private float _posXLeft;
    private float _posXRight;
    private float _posYUp;
    private float _posYDown;
    private float _timeCheckTarget = 0f;

    protected override void Start()
    {
        base.Start();
        LimitPos();
    }
    protected override void Update()
    {
        _timeCheckTarget += Time.deltaTime;
    }
    protected override void Move()
    {
        if(_target == _player.transform.position)
        {
            ChangeState(EnemyState.Chasing);
            _speedCur = _enemySO.SpeedChasing;
        }
        else
        {
            if (!_newPos)
            {
                _newPos = true;
                ChangeState(EnemyState.Moving);
                _speedCur = _speedMove;
                float randomX = Random.Range(_posXLeft, _posXRight);
                float randomY = Random.Range(_posYDown, _posYUp);
                _target = new Vector3(randomX, randomY, transform.position.z);
            }
            if (Vector3.Distance(transform.position, _target) <= 0.5f)
            {
                _newPos = false;
            }
        }
        
        if (_canRotation) 
        {
            CheckFlipY(_rb.velocity.x < 0f);
            RotateToTarget(); 
        }
        else 
        { 
            CheckFlipX(_rb.velocity.x < 0f); 
        }

        _direction = (_target - transform.position).normalized;
        //_direction.Normalize();

        if (_knockBack) return;
        _rb.velocity = _direction * _speedCur;
    }
    private void RotateToTarget()
    {
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void LimitPos()
    {
        _posXRight = _startPosition.x + _movingDist;
        _posXLeft = _startPosition.x - _movingDist;
        _posYUp = _startPosition.y + _movingDist;
        _posYDown = _startPosition.y - _movingDist;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (_timeCheckTarget >= 1f)
            {
                _target = collision.transform.position;
                //_newPos = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _newPos = false;
        }
    }
}
