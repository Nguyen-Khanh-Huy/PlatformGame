using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Animator _anim;
    protected SpriteRenderer _sp;
    protected Rigidbody2D _rb;
    
    protected EnemySO _enemySO;

    protected float _speedCur;
    protected float _movingDist;

    protected Vector3 _startPosition;
    protected Vector3 _target;
    protected Vector3 _direction;

    protected virtual void Start()
    {
        _startPosition = transform.position;
        _speedCur = _enemySO.SpeedMove;
    }
    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    public virtual void TakeDamageEnemy(int dmg)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Dead")) return;
        if (_enemySO.Hp > 0)
        {
            _enemySO.Hp -= dmg;
            if (_enemySO.Hp <= 0)
            {
                _enemySO.Hp = 0;
                Death();
            }
        }
    }
    protected virtual void Death()
    {
        gameObject.layer = LayerMask.NameToLayer("Dead");
        _rb.velocity = Vector2.zero;
    }
}
