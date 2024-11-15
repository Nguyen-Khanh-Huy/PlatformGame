using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    [SerializeField] protected SpriteRenderer _sp;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected CapsuleCollider2D _col;
    [SerializeField] protected Transform _player;
    [SerializeField] protected EnemySO _enemySO;

    [SerializeField] protected float _speedCur;
    [SerializeField] protected float _speedMove;
    [SerializeField] protected float _movingDist;

    [SerializeField] protected Vector3 _target;
    protected Vector3 _startPosition;
    protected Vector3 _direction;

    protected virtual void Start()
    {
        _player = GameObject.Find("Player").transform;
        _startPosition = transform.position;
        _speedCur = _speedMove;
    }
    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    protected virtual void ChangeState(EnemyState State)
    {
        if (_anim == null) return;
        _anim.SetInteger("State", (int)State);
    }
    protected void CheckFlipX(bool check)
    {
        if(_col == null && _sp == null) return;
        if (check)
        {
            _sp.flipX = true;
            _col.offset = _col.offset.x > 0f ? new Vector2(-_col.offset.x, _col.offset.y) : _col.offset;
        }
        else
        {
            _sp.flipX = false;
            _col.offset = _col.offset.x < 0f ? new Vector2(-_col.offset.x, _col.offset.y) : _col.offset;
        }
    }
    protected void CheckFlipY(bool check)
    {
        if (_col == null && _sp == null) return;
        if (check)
        {
            _sp.flipY = true;
            _col.offset = _col.offset.y > 0f ? new Vector2(_col.offset.x, -_col.offset.y) : _col.offset;
        }
        else
        {
            _sp.flipY = false;
            _col.offset = _col.offset.y < 0f ? new Vector2(_col.offset.x, -_col.offset.y) : _col.offset;
        }
    }
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
