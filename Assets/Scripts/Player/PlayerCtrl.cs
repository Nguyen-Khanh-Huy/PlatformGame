using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCtrl : MonoBehaviour
{
    [SerializeField] protected Obstacle Obs;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected SpriteRenderer _sp;

    [SerializeField] protected CapsuleCollider2D _colDefaul;
    [SerializeField] protected CapsuleCollider2D _colFly;
    [SerializeField] protected CapsuleCollider2D _colWater;

    [SerializeField] protected float _speedCur;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _speedMove;
    [SerializeField] protected float _speedFly;
    [SerializeField] protected float _speedLadder;
    [SerializeField] protected float _speedSwim;
    [SerializeField] private bool _checkLandAndJump;
    private int hoz, vert;

    [SerializeField] protected float _startGravity;
    [SerializeField] protected bool _isKnockBack;
    [SerializeField] protected bool _isInvincible;

    protected virtual void Start()
    {
        _startGravity = _rb.gravityScale;
        _speedCur = _speedMove;
    }
    protected virtual void Update()
    {
        JumpCheck();
        FlyAndOnAirCheck();

        GroundCheck();
        LadderCheck();
        WaterCheck();
    }
    protected virtual void FixedUpdate()
    {
        SmoothJump();
    }
    protected virtual void ChangeState(PlayerState State)
    {
        _anim.SetInteger("State", (int)State);
    }
    private void ActiveCol(CapsuleCollider2D Col)
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
            if (_colDefaul.offset.x < 0f && _colFly.offset.x > 0f && _colWater.offset.x > 0f)
            {
                _colDefaul.offset = new Vector2(-_colDefaul.offset.x, _colDefaul.offset.y);
                _colFly.offset = new Vector2(-_colFly.offset.x, _colFly.offset.y);
                _colWater.offset = new Vector2(-_colWater.offset.x, _colWater.offset.y);
            }
        }
        else
        {
            _sp.flipX = false;
            if (_colDefaul.offset.x > 0f && _colFly.offset.x < 0f && _colWater.offset.x < 0f)
            {
                _colDefaul.offset = new Vector2(-_colDefaul.offset.x, _colDefaul.offset.y);
                _colFly.offset = new Vector2(-_colFly.offset.x, _colFly.offset.y);
                _colWater.offset = new Vector2(-_colWater.offset.x, _colWater.offset.y);
            }
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
    protected virtual void JumpCheck()
    {
        if (GamePad.Ins.CanJump && (Obs.IsOnGround || Obs.IsOnWaterSurface))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }
    protected virtual void SmoothJump()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * 4f * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !GamePad.Ins.CanJumpHolding)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * 4f * Time.deltaTime;
        }
    }
    protected virtual void GroundCheck()
    {
        if (Obs.IsOnGround)
        {
            _speedCur = _speedMove;
            GamePad.Ins.CanMoveUp = false;
            GamePad.Ins.CanMoveDown = false;
            GamePad.Ins.CanFly = false;
            ActiveCol(_colDefaul);
            MoveFull();
            if (GamePad.Ins.CanAttack)
            {
                ChangeState(PlayerState.Attack);
            }
            else if (GamePad.Ins.CanBullet)
            {
                ChangeState(PlayerState.Bullet);
            }
            else if (!_checkLandAndJump)
            {
                GamePad.Ins.CanJumpHolding = false;
                ChangeState(PlayerState.Land);
                _checkLandAndJump = true;
            }
            else if (GamePad.Ins.IsStatic)
            {
                ChangeState(PlayerState.Idle);
                _rb.velocity = Vector2.zero;
            }
            else if (GamePad.Ins.CanMoveLeft || GamePad.Ins.CanMoveRight)
            {
                _checkLandAndJump = true;
                ChangeState(PlayerState.Walk);
            }
        }
        else { _checkLandAndJump = false; }
    }
    protected virtual void LadderCheck()
    {
        if (Obs.IsOnLadder)
        {
            GamePad.Ins.CanFly = false;
            GamePad.Ins.CanJump = false;
            GamePad.Ins.CanJumpHolding = false;
            _speedCur = _speedLadder;
            ActiveCol(_colDefaul);
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
            if (GamePad.Ins.CanBullet)
            {
                ChangeState(PlayerState.Bullet);
            }
            else if (GamePad.Ins.IsStatic)
            {
                ChangeState(PlayerState.LadderIdle);
            }
            else if (GamePad.Ins.CanMoveLeft || GamePad.Ins.CanMoveRight || GamePad.Ins.CanMoveUp || GamePad.Ins.CanMoveDown)
            {
                MoveFull();
                ChangeState(PlayerState.Ladder);
            }
        }
    }
    protected virtual void FlyAndOnAirCheck()
    {
        if (!Obs.IsOnLadder && !Obs.IsOnGround && !Obs.IsOnWaterSurface && !Obs.IsOnWaterDeep)
        {
            GamePad.Ins.CanMoveUp = false;
            GamePad.Ins.CanMoveDown = false;
            if (GamePad.Ins.CanAttack)
            {
                ChangeState(PlayerState.Attack);
            }
            else if (GamePad.Ins.CanBullet)
            {
                ChangeState(PlayerState.Bullet);
            }
            else if (!GamePad.Ins.CanFly && _rb.velocity.y > 0f || GamePad.Ins.CanFly && _rb.velocity.y > 0f)
            {
                ChangeState(PlayerState.Jump);
                ActiveCol(_colDefaul);
                MoveFull();
                _speedCur = _speedMove;
                _rb.gravityScale = _startGravity;
            }
            else if (!GamePad.Ins.CanFly && _rb.velocity.y <= 0f)
            {
                ChangeState(PlayerState.OnAir);
                ActiveCol(_colDefaul);
                MoveFull();
                _speedCur = _speedMove;
                _rb.gravityScale = _startGravity;
            }
            else if (GamePad.Ins.CanFly && _rb.velocity.y <= 0f)
            {
                ChangeState(PlayerState.Fly);
                ActiveCol(_colFly);
                MoveFly();
                _speedCur = _speedFly;
                _rb.gravityScale = 0;
            }
        }
    }
    protected virtual void WaterCheck()
    {
        if (Obs.IsOnWaterSurface)
        {
            _speedCur = _speedSwim;
            _rb.gravityScale = 0;
            ChangeState(PlayerState.SwimTop);
            ActiveCol(_colWater);
            GamePad.Ins.CanFly = false;
            GamePad.Ins.CanMoveUp = false;
            if (GamePad.Ins.CanMoveLeft || GamePad.Ins.CanMoveRight || GamePad.Ins.CanMoveDown)
            {
                MoveFull();
            }
            else if (GamePad.Ins.IsStatic) { _rb.velocity = Vector2.zero; }
        }
        else if (Obs.IsOnWaterDeep)
        {
            _speedCur = _speedSwim;
            _rb.gravityScale = 0;
            ChangeState(PlayerState.SwimDeep);
            ActiveCol(_colWater);
            GamePad.Ins.CanFly = false;
            GamePad.Ins.CanJump = false;
            if (GamePad.Ins.CanMoveLeft || GamePad.Ins.CanMoveRight || GamePad.Ins.CanMoveUp || GamePad.Ins.CanMoveDown)
            {
                MoveFull();
            }
            else { _rb.velocity = Vector2.zero; }
        }
    }
}
