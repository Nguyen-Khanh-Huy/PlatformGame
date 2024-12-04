using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    [SerializeField] protected SpriteRenderer _sp;
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected Collider2D _col;
    [SerializeField] protected Transform _player;
    [SerializeField] protected EnemySO _enemySO;
    [SerializeField] private GameObject _vfx;

    [SerializeField] protected int _curHp;
    protected float _speedCur;
    [SerializeField] protected float _speedMove;
    [SerializeField] protected float _movingDist;
    [SerializeField] protected Vector3 _target;
    
    protected bool _knockBack;
    protected Vector3 _startPosition;
    protected Vector3 _direction;

    protected abstract void Move();

    protected virtual void Start()
    {
        Door door = GameObject.Find("DoorLock").GetComponent<Door>();
        _player = GameObject.Find("Player").transform;
        _curHp = _enemySO.Hp;
        _startPosition = transform.position;
        _speedCur = _speedMove;
    }
    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate()
    {
        if (PlayerCtrl.Ins.IsDead)
        {
            PlayerDeath();
            return;
        }
        Move();
    }
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
    private IEnumerator KnockbackEffect()
    {
        yield return new WaitForSeconds(_enemySO.KnockBackTime);
        _knockBack = false;
    }
    public virtual void TakeDamageEnemy(int dmg, Vector2 attackDir)
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxGetHit);
        if (_curHp > 0)
        {
            _curHp -= dmg;
            if (_curHp <= 0)
            {
                _curHp = 0;
                Death();
            }
        }
        _knockBack = true;
        if (_knockBack)
        {
            _rb.velocity = Vector2.zero;
            _rb.AddForce(attackDir * _enemySO.KnockBackForce, ForceMode2D.Impulse);
            StartCoroutine(KnockbackEffect());
        }
    }
    protected virtual void Death()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxDeadEnemy);
        Destroy(gameObject);
        Vfx();
    }
    protected virtual void Vfx()
    {
        _vfx = Instantiate(_vfx, transform.position, Quaternion.identity);
        Destroy(_vfx, 0.5f);
    }
    protected virtual void PlayerDeath()
    {
        _speedMove = 0;
        _enemySO.SpeedChasing = 0;
        _rb.velocity = Vector3.zero;
        _rb.gravityScale = 0f;
    }
}
