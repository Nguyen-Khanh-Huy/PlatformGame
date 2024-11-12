using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerCtrl
{
    private bool _checkLandAndJump;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        JumpCheck();
        FlyAndOnAirCheck();

        GroundCheck();
        LadderCheck();
        WaterCheck();
    }
    protected override void FixedUpdate()
    {
        SmoothJump();
    }
    private void SmoothJump()
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
    private void JumpCheck()
    {
        if (GamePad.Ins.CanJump && (Obs.IsOnGround || Obs.IsMovingPlatform || Obs.IsOnWaterSurface))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _playerSO.JumpForce);
        }
    }
    private void FlyAndOnAirCheck()
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

                _speedCur = _playerSO.SpeedMove;
                _rb.gravityScale = _startGravity;

                MoveFull();
            }
            else if (!GamePad.Ins.CanFly && _rb.velocity.y <= 0f)
            {
                ChangeState(PlayerState.OnAir);
                ActiveCol(_colDefaul);

                _speedCur = _playerSO.SpeedMove;
                _rb.gravityScale = _startGravity;

                MoveFull();
            }
            else if (GamePad.Ins.CanFly && _rb.velocity.y <= 0f)
            {
                ChangeState(PlayerState.Fly);
                ActiveCol(_colFly);

                _speedCur = _playerSO.SpeedFly;
                _rb.gravityScale = 0;

                MoveFly();
            }
        }
    }
    private void GroundCheck()
    {
        if (Obs.IsOnGround)
        {
            _speedCur = _playerSO.SpeedMove;
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
                _checkLandAndJump = true;
                GamePad.Ins.CanJump = false;
                GamePad.Ins.CanJumpHolding = false;
                ChangeState(PlayerState.Land);
            }
            else if (GamePad.Ins.IsStatic || (_anim.GetInteger("State") == (int)PlayerState.Land) && GamePad.Ins.CanJumpHolding)
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
    private void LadderCheck()
    {
        if (Obs.IsOnLadder)
        {
            GamePad.Ins.CanFly = false;
            GamePad.Ins.CanJump = false;
            GamePad.Ins.CanJumpHolding = false;
            _speedCur = _playerSO.SpeedLadder;
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
    private void WaterCheck()
    {
        if (Obs.IsOnWaterSurface)
        {
            _speedCur = _playerSO.SpeedSwim;
            _rb.gravityScale = 0;
            ChangeState(PlayerState.SwimSurface);
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
            _speedCur = _playerSO.SpeedSwim;
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
