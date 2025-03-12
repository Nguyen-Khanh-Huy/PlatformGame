using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player SO", menuName = "PIS/Player SO")]
public class PlayerSO : ScriptableObject
{
    [Header("Common:")]
    public int Hp;
    public int Life;
    public int Bullet;
    public int Damage;

    [Header("Moving:")]
    public float JumpForce;
    public float SpeedMove;
    public float SpeedFly;
    public float SpeedLadder;
    public float SpeedSwim;
    public float KnockBackTime;
    public float KnockBackForce;
}
