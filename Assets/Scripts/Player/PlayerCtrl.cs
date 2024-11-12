using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCtrl : MonoBehaviour
{
    [SerializeField] protected PlayerObstacle Obs;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected SpriteRenderer _sp;

    [SerializeField] protected CapsuleCollider2D _colDefaul;
    [SerializeField] protected CapsuleCollider2D _colFly;
    [SerializeField] protected CapsuleCollider2D _colWater;

    [SerializeField] protected PlayerSO _playerSO;

    [SerializeField] protected float _speedCur;
    [SerializeField] protected float _startGravity;
    [SerializeField] protected bool _isDead;

    protected int hoz, vert;

    protected virtual void Start()
    {
        _startGravity = _rb.gravityScale;
        _speedCur = _playerSO.SpeedMove;
    }
    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate()
    {
        
    }
    protected virtual void ChangeState(PlayerState State)
    {
        _anim.SetInteger("State", (int)State);
    }
    protected virtual void ActiveCol(CapsuleCollider2D Col)
    {
        if (_colDefaul)
            _colDefaul.enabled = Col == _colDefaul;
        if (_colFly)
            _colFly.enabled = Col == _colFly;
        if (_colWater)
            _colWater.enabled = Col == _colWater;
    }
    private void CheckFlip(bool check)
    {
        if (check)
        {
            _sp.flipX = true;
            _colDefaul.offset = _colDefaul.offset.x < 0f ? new Vector2(-_colDefaul.offset.x, _colDefaul.offset.y) : _colDefaul.offset;
            _colFly.offset = _colFly.offset.x > 0f ? new Vector2(-_colFly.offset.x, _colFly.offset.y) : _colFly.offset;
            _colWater.offset = _colWater.offset.x > 0f ? new Vector2(-_colWater.offset.x, _colWater.offset.y) : _colWater.offset;
        }
        else
        {
            _sp.flipX = false;
            _colDefaul.offset = _colDefaul.offset.x > 0f ? new Vector2(-_colDefaul.offset.x, _colDefaul.offset.y) : _colDefaul.offset;
            _colFly.offset = _colFly.offset.x < 0f ? new Vector2(-_colFly.offset.x, _colFly.offset.y) : _colFly.offset;
            _colWater.offset = _colWater.offset.x < 0f ? new Vector2(-_colWater.offset.x, _colWater.offset.y) : _colWater.offset;
        }
    }
    protected virtual void MoveFly()
    {
        hoz = GamePad.Ins.CanMoveLeft ? -1 : GamePad.Ins.CanMoveRight ? 1 : 0;
        if (hoz != 0)
        {
            _rb.velocity = new Vector2(hoz * (_speedCur + 3), -_speedCur);
            CheckFlip(hoz < 0);
        }
        else
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_speedCur);
        }
    }
    protected virtual void MoveFull()
    {
        hoz = GamePad.Ins.CanMoveLeft ? -1 : GamePad.Ins.CanMoveRight ? 1 : 0;
        vert = GamePad.Ins.CanMoveDown ? -1 : GamePad.Ins.CanMoveUp ? 1 : 0;
        if (hoz != 0)
        {
            _rb.velocity = new Vector2(hoz * _speedCur, _rb.velocity.y);
            CheckFlip(hoz < 0);
        }
        if (vert != 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, vert * _speedCur);
        }
        else if (hoz != 0 && Obs.IsOnWaterDeep)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
    }
    public virtual void TakeDamagePlayer(int dmg)
    {
        if (_isDead) return;
        if (_playerSO.Hp > 0)
        {
            _playerSO.Hp -= dmg;
            if (_playerSO.Hp <= 0)
            {
                _playerSO.Hp = 0;
                _isDead = true;
                Death();
            }
        }
    }
    protected virtual void Death()
    {
        gameObject.layer = LayerMask.NameToLayer("Dead");
        _rb.velocity = Vector2.zero;
        ChangeState(PlayerState.Dead);
    }
}
