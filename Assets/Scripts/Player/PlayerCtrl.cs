using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCtrl : Singleton<PlayerCtrl>
{
    [SerializeField] protected PlayerObstacle Obs;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected SpriteRenderer _sp;

    [SerializeField] protected CapsuleCollider2D _colDefaul;
    [SerializeField] protected CapsuleCollider2D _colFly;
    [SerializeField] protected CapsuleCollider2D _colWater;

    [SerializeField] protected int _curHp;
    [SerializeField] protected float _speedCur;
    [SerializeField] protected float _startGravity;
    [SerializeField] protected bool _knockBack;
    [SerializeField] protected bool _isDead;

    protected int _hoz, _vert;
    protected bool _checkDoor;
    public int CurHp { get => _curHp; set => _curHp = value; }
    public bool IsDead { get => _isDead; }

    public override void Awake()
    {
        DontDestroy(false);
    }
    protected virtual void Start()
    {
        _startGravity = _rb.gravityScale;
        _speedCur = PlayerManager.Ins.PlayerSO.SpeedMove;
        if(_curHp == 0)
        {
            PlayerManager.Ins.hp = PlayerManager.Ins.PlayerSO.Hp;
            GameData.Ins.SaveGame();
        }
        _curHp = PlayerManager.Ins.hp;
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
        if (_knockBack) return;
        
        _hoz = GamePad.Ins.CanMoveLeft ? -1 : GamePad.Ins.CanMoveRight ? 1 : 0;
        if (_hoz != 0)
        {
            CheckFlip(_hoz < 0);
            _rb.velocity = new Vector2(_hoz * (_speedCur + 3), -_speedCur);
        }
        else
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_speedCur);
        }
    }
    protected virtual void MoveFull()
    {
        if (_knockBack) return;

        _hoz = GamePad.Ins.CanMoveLeft ? -1 : GamePad.Ins.CanMoveRight ? 1 : 0;
        _vert = GamePad.Ins.CanMoveDown ? -1 : GamePad.Ins.CanMoveUp ? 1 : 0;
        if (_hoz != 0)
        {
            CheckFlip(_hoz < 0);
            _rb.velocity = new Vector2(_hoz * _speedCur, _rb.velocity.y);
        }
        if (_vert != 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _vert * _speedCur);
        }
        else if (_hoz != 0 && Obs.IsOnWaterDeep)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
    }
    public virtual void IsIdle()
    {
        if (_knockBack) return;
        _rb.velocity = Vector2.zero;
    }
    public virtual void PlayerCheckDoor()
    {
        _checkDoor = true;
        IsIdle();
        ChangeState(PlayerState.SayHello);
    }
    private IEnumerator KnockbackEffect()
    {
        yield return new WaitForSeconds(PlayerManager.Ins.PlayerSO.KnockBackTime);
        _knockBack = false;
    }
    public virtual void TakeDamagePlayer(int dmg, Vector2 attackDir)
    {
        if (_isDead) return;
        _curHp -= dmg;
        PlayerManager.Ins.hp = _curHp;
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxGetHit);
        if (_curHp <= 0)
        {
            if (PlayerManager.Ins.life > 0)
            {
                _curHp = PlayerManager.Ins.PlayerSO.Hp;
                PlayerManager.Ins.hp = _curHp;
                PlayerManager.Ins.life--;
                PlayerManager.Ins.Revival();
            }
            else
            {
                _curHp = 0;
                PlayerManager.Ins.hp = _curHp;
                Death();
            }
        }
        GameData.Ins.SaveGame();

        _knockBack = true;
        if (_knockBack)
        {
            _rb.velocity = Vector2.zero;
            _rb.AddForce(attackDir * PlayerManager.Ins.PlayerSO.KnockBackForce, ForceMode2D.Impulse);
            StartCoroutine(KnockbackEffect());
        }
    }
    protected virtual void Death()
    {
        _isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Dead");
        ChangeState(PlayerState.Dead);

        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxDeadPlayer);
        StartCoroutine(LevelFailDialog());
    }
    private IEnumerator LevelFailDialog()
    {
        yield return new WaitForSeconds(1);
        UIGamePlayManager.Ins.Show(UIGamePlayManager.Ins.UILevelFailDialog);
    }
}
