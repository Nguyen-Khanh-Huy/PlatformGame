using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCtrl : MonoBehaviour
{
    [SerializeField] protected ObsController Obs;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected SpriteRenderer _sp;

    [SerializeField] protected CapsuleCollider2D _colDefaul;
    [SerializeField] protected CapsuleCollider2D _colFly;
    [SerializeField] protected CapsuleCollider2D _colWater;

    [SerializeField] private CircleCollider2D _ColObsDefaul;
    [SerializeField] private CircleCollider2D _ColObsWater;

    [SerializeField] protected float _speedCur;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _speedMove;
    [SerializeField] protected float _speedFly;
    [SerializeField] protected float _speedLadder;
    [SerializeField] protected float _speedSwim;
    protected float jumpFallMultipiler = 5f;
    protected float jumpLowMultipiler = 5f;

    [SerializeField] protected float _startGravity;
    [SerializeField] protected bool _isKnockBack;
    [SerializeField] protected bool _isInvincible;
    [SerializeField] protected bool _isFaceLeft;

    protected virtual void Start()
    {
        //_ani = transform.Find("Model").GetComponent<Animator>();
        if (_rb != null) { _startGravity = _rb.gravityScale; }
        _speedCur = _speedMove;
    }
    protected virtual void Update()
    {
        //MoveHoz();
        //MoveFull();
        JumpCheck();
        LadderCheck();
        GroundCheck();
        FlyCheck();
        WaterCheck();
        if(Obs.IsOnLadder == false)
        {
            //Debug.Log("=======");
        }
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
        if (check == true)
        {
            _sp.flipX = true;
            if(_ColObsDefaul.offset.x < 0f && _ColObsWater.offset.x > 0f)
            {
                _ColObsDefaul.offset = new Vector2(-_ColObsDefaul.offset.x, _ColObsDefaul.offset.y);
                _ColObsWater.offset = new Vector2(-_ColObsWater.offset.x, _ColObsWater.offset.y);
            }
            if(_colDefaul.offset.x < 0f && _colFly.offset.x > 0f && _colWater.offset.x > 0f)
            {
                _colDefaul.offset = new Vector2(-_colDefaul.offset.x, _colDefaul.offset.y);
                _colFly.offset = new Vector2(-_colFly.offset.x, _colFly.offset.y);
                _colWater.offset = new Vector2(-_colWater.offset.x, _colWater.offset.y);
            }
        }
        else
        {
            _sp.flipX = false;
            if (_ColObsDefaul.offset.x > 0f && _ColObsWater.offset.x < 0f)
            {
                _ColObsDefaul.offset = new Vector2(-_ColObsDefaul.offset.x, _ColObsDefaul.offset.y);
                _ColObsWater.offset = new Vector2(-_ColObsWater.offset.x, _ColObsWater.offset.y);
            }
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
        if (GamePad.Ins.CanFly)
        {
            if (GamePad.Ins.CanMoveLeft)
            {
                _rb.velocity = new Vector2((_speedCur +3) * -1, -_speedCur);
                CheckFlip(true);
            }
            if (GamePad.Ins.CanMoveRight)
            {
                _rb.velocity = new Vector2(_speedCur + 3, -_speedCur);
                CheckFlip(false);
            }
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_speedCur);
            }
        }
    }
    protected virtual void MoveSwim()
    {
        if (GamePad.Ins.CanMoveLeft)
        {
            _rb.velocity = new Vector2(-_speedCur, 0f);
            CheckFlip(true);
        }
        if (GamePad.Ins.CanMoveRight)
        {
            _rb.velocity = new Vector2(_speedCur, 0f);
            CheckFlip(false);
        }
        if (GamePad.Ins.CanMoveUp)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _speedCur);
        }
        if (GamePad.Ins.CanMoveDown)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_speedCur);
        }
    }
    protected virtual void MoveHoz()
    {
        if (GamePad.Ins.CanMoveLeft)
        {
            _rb.velocity = new Vector2(_speedCur * -1, _rb.velocity.y);
            CheckFlip(true);
        }
        if (GamePad.Ins.CanMoveRight)
        {
            _rb.velocity = new Vector2(_speedCur, _rb.velocity.y);
            CheckFlip(false);
        }
    }
    protected virtual void MoveFull()
    {
        if (GamePad.Ins.CanMoveLeft)
        {
            _rb.velocity = new Vector2(_speedCur * -1, _rb.velocity.y);
            CheckFlip(true);
        }
        if (GamePad.Ins.CanMoveRight)
        {
            _rb.velocity = new Vector2(_speedCur, _rb.velocity.y);
            CheckFlip(false);
        }
        if (GamePad.Ins.CanMoveUp)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _speedCur);
        }
        if (GamePad.Ins.CanMoveDown)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _speedCur * -1);
        }
    }
    protected virtual void JumpCheck()
    {
        if (GamePad.Ins.CanJump)
        {
            if (Obs.IsOnGround || Obs.IsOnWaterTop)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            }
        }
    }
    protected virtual void SmoothJump()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultipiler - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !GamePad.Ins.CanJumpHolding)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpLowMultipiler - 1) * Time.deltaTime;
        }
    }
    IEnumerator DelayIdle()
    {
        yield return new WaitForSeconds(0.08f);
        ChangeState(PlayerState.Idle);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ground ground = collision.gameObject.GetComponent<Ground>();
        if (ground && Obs.IsOnGround && GamePad.Ins.IsStatic)
        {
            ChangeState(PlayerState.Land);
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
            if (GamePad.Ins.IsStatic)
            {
                StartCoroutine(DelayIdle());
                _rb.velocity = Vector2.zero;
            }
            if (GamePad.Ins.CanMoveLeft || GamePad.Ins.CanMoveRight)
            {
                ChangeState(PlayerState.Walk);
                MoveHoz();
            }
        }
    }
    protected virtual void FlyCheck()
    {
        if(GamePad.Ins.CanFly)
        {
            if (!Obs.IsOnGround && !Obs.IsOnLadder && !Obs.IsOnWaterTop && !Obs.IsOnWaterDeep && _rb.velocity.y < 0.5f)
            {
                _speedCur = _speedFly;
                ChangeState(PlayerState.Fly);
                ActiveCol(_colFly);
                MoveFly();
            }
        }
    }
    protected virtual void LadderCheck()
    {
        if (Obs.IsOnLadder)
        {
            GamePad.Ins.CanFly = false;
            _speedCur = _speedLadder;
            ActiveCol(_colDefaul);
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
            if (GamePad.Ins.IsStatic)
            {
                ChangeState(PlayerState.LadderIdle);
            }
            if (GamePad.Ins.CanMoveLeft || GamePad.Ins.CanMoveRight || GamePad.Ins.CanMoveUp || GamePad.Ins.CanMoveDown)
            {
                MoveFull();
                ChangeState(PlayerState.Ladder);
            }
        }
        if (!Obs.IsOnLadder && !Obs.IsOnWaterTop && !Obs.IsOnWaterDeep)
        {
            _speedCur = _speedMove;
            _rb.gravityScale = _startGravity;
            if (!GamePad.Ins.CanFly && !Obs.IsOnLadder && !Obs.IsOnGround && !Obs.IsOnWaterTop && !Obs.IsOnWaterDeep)
            {
                MoveHoz();
                ChangeState(PlayerState.OnAir);
            }
        }
    }
    protected virtual void WaterCheck()
    {
        if (Obs.IsOnWaterTop)
        {
            _speedCur = _speedSwim;
            _rb.gravityScale = 0;
            ChangeState(PlayerState.SwimTop);
            ActiveCol(_colWater);
            GamePad.Ins.CanMoveUp = false;
            if (GamePad.Ins.IsStatic)
            {
                _rb.velocity = Vector2.zero;
            }
            if (GamePad.Ins.CanMoveLeft || GamePad.Ins.CanMoveRight || GamePad.Ins.CanMoveUp || GamePad.Ins.CanMoveDown)
            {
                MoveSwim();
            }
        }
        if (Obs.IsOnWaterDeep)
        {
            _speedCur = _speedSwim;
            _rb.gravityScale = 0;
            ChangeState(PlayerState.SwimDeep);
            ActiveCol(_colWater);
            if (GamePad.Ins.IsStatic)
            {
                _rb.velocity = Vector2.zero;
            }
            if (GamePad.Ins.CanMoveLeft || GamePad.Ins.CanMoveRight || GamePad.Ins.CanMoveUp || GamePad.Ins.CanMoveDown)
            {
                MoveSwim();
            }
        }
    }
}
