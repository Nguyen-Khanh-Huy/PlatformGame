using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCtrl : MonoBehaviour
{
    [SerializeField] protected Animator _ani;
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
    [Range(0f, 5f)]
    public float jumpFallMultipiler = 2.5f;
    [Range(0f, 5f)]
    public float jumpLowMultipiler = 2.5f;

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
        Move();
    }
    protected virtual void FixedUpdate()
    {
        SmoothJump();
    }
    protected virtual void ChangeState(PlayerState State)
    {
        _ani.SetInteger("State", (int)State);
    }
    protected virtual void Move()
    {
        if (GamePad.Ins.CanMoveLeft)
        {
            _rb.velocity = new Vector2(_speedCur * -1, _rb.velocity.y);
        }
        if (GamePad.Ins.CanMoveRight)
        {
            _rb.velocity = new Vector2(_speedCur, _rb.velocity.y);
        }
        if (GamePad.Ins.CanMoveUp)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _speedCur);
        }
        if (GamePad.Ins.CanMoveDown)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _speedCur * -1);
        }
        if (GamePad.Ins.CanJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }
    private void SmoothJump()
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
}
