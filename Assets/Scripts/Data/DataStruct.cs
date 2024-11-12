using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Jump,
    Attack,
    Bullet,
    Ladder,
    LadderIdle,
    Fly,
    OnAir,
    Land,
    SwimSurface,
    SwimDeep,
    Dead,
    SayHello
}
public enum EnemyState
{
    Moving,
    Chasing
}
public enum EnemyDir
{
    Up,
    Down,
    Left,
    Right
}
