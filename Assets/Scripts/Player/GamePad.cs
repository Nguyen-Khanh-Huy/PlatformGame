using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePad : Singleton<GamePad>
{
    public bool IsOnMobile;
    public Joystick Joystick;

    [SerializeField] private bool _canMoveLeft;
    [SerializeField] private bool _canMoveRight;
    [SerializeField] private bool _canMoveUp;
    [SerializeField] private bool _canMoveDown;
    [SerializeField] private bool _canJump;
    [SerializeField] private bool _canJumpHolding;
    [SerializeField] private bool _canFly;
    [SerializeField] private bool _canBullet;
    [SerializeField] private bool _canAttack;

    [SerializeField] private bool _checkJumpHolding;

    public bool CanMoveLeft { get => _canMoveLeft; set => _canMoveLeft = value; }
    public bool CanMoveRight { get => _canMoveRight; set => _canMoveRight = value; }
    public bool CanMoveUp { get => _canMoveUp; set => _canMoveUp = value; }
    public bool CanMoveDown { get => _canMoveDown; set => _canMoveDown = value; }
    public bool CanJump { get => _canJump; set => _canJump = value; }
    public bool CanJumpHolding { get => _canJumpHolding; set => _canJumpHolding = value; }
    public bool CanFly { get => _canFly; set => _canFly = value; }
    public bool CanBullet { get => _canBullet; set => _canBullet = value; }
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }

    public bool IsIdle
    {
        get => !_canMoveLeft && !_canMoveRight && !_canMoveUp && !_canMoveDown && !_canJump && !_canFly && !_canJumpHolding;
    }

    private void Update()
    {
        if (!IsOnMobile)
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
            }
            if (_checkJumpHolding)
            {
                _canJumpHolding = Input.GetKey(KeyCode.Space);
            }
        }
        else
        {
            if (Joystick == null) return;
            _canMoveLeft = Joystick.xValue < 0 ? true : false;
            _canMoveRight = Joystick.xValue > 0 ? true : false;
            _canMoveUp = Joystick.yValue > 0 ? true : false;
            _canMoveDown = Joystick.yValue < 0 ? true : false;
        }
    }
    public void GetcomponentJoystick()
    {
        Joystick = GameObject.Find("ImgJoystick").GetComponent<Joystick>();
    }
}
