using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody2D _rb;

    private void Start()
    {
        _anim = transform.Find("Model").GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (GamePad.Ins.CanMoveRight)
        {
            _rb.velocity = new Vector2(2f, _rb.velocity.y);
            ChangeState(PlayerState.Walk);
        }
    }
    private void ChangeState(PlayerState State)
    {
        _anim.SetInteger("State", (int)State);
    }
    //private void SmoothJump()
    //{
    //    if (obstacle.IsOnGround || obstacle.IsOnWater && IsJumping) return;
    //    if (_rb.velocity.y < 0)
    //    {
    //        _rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultipiler - 1) * Time.deltaTime;
    //    }
    //    else if (_rb.velocity.y > 0 && !GamepadController.Ins.IsJumpHolding)
    //    {
    //        _rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpLowMultipiler - 1) * Time.deltaTime;
    //    }
    //}
}
