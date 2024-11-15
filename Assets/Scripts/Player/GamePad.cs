using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePad : Singleton<GamePad>
{
    //public Joystick joystick; 

    //public float jumpHoldingTime;
    private bool _canMoveLeft;
    private bool _canMoveRight;
    private bool _canMoveUp;
    private bool _canMoveDown;
    private bool _canJump;
    private bool _canJumpHolding;
    private bool _canFly;
    private bool _canBullet;
    private bool _canAttack;

    private bool _checkJumpHolding;
    //private float _curHoldingTime;

    public bool CanMoveLeft { get => _canMoveLeft; set => _canMoveLeft = value; }
    public bool CanMoveRight { get => _canMoveRight; set => _canMoveRight = value; }
    public bool CanMoveUp { get => _canMoveUp; set => _canMoveUp = value; }
    public bool CanMoveDown { get => _canMoveDown; set => _canMoveDown = value; }
    public bool CanJump { get => _canJump; set => _canJump = value; }
    public bool CanJumpHolding { get => _canJumpHolding; set => _canJumpHolding = value; }
    public bool CanFly { get => _canFly; set => _canFly = value; }
    public bool CanBullet { get => _canBullet; set => _canBullet = value; }
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }

    public bool IsStatic
    {
        get => !_canMoveLeft && !_canMoveRight && !_canMoveUp && !_canMoveDown && !_canJump && !_canFly && !_canJumpHolding;
    }
    public override void Awake()
    {
        DontDestroy(false);
    }
    private void Update()
    {
        float hozCheck = Input.GetAxisRaw("Horizontal");
        float vertCheck = Input.GetAxisRaw("Vertical");
        _canMoveLeft = hozCheck < 0 ? true : false;
        _canMoveRight = hozCheck > 0 ? true : false;
        _canMoveUp = vertCheck > 0 ? true : false;
        _canMoveDown = vertCheck < 0 ? true : false;

        _canJump = Input.GetKeyDown(KeyCode.Space);
        _canFly = Input.GetKey(KeyCode.F);

        _canBullet = Input.GetKeyDown(KeyCode.C);
        _canAttack = Input.GetKeyDown(KeyCode.V);

        if (_canJump)
        {
            _canJumpHolding = false;
            _checkJumpHolding = true;
            //_curHoldingTime = 0;
        }
        if (_checkJumpHolding)
        {
            //_curHoldingTime += Time.deltaTime;
            //if (_curHoldingTime > jumpHoldingTime)
            //{
            //    _canJumpHolding = Input.GetKey(KeyCode.Space);
            //}
            _canJumpHolding = Input.GetKey(KeyCode.Space);
        }

        //if (joystick == null) return;
        //_canMoveLeft = joystick.xValue < 0 ? true : false;
        //_canMoveRight = joystick.xValue > 0 ? true : false;
        //_canMoveUp = joystick.yValue > 0 ? true : false;
        //_canMoveDown = joystick.yValue < 0 ? true : false;
    }
}
