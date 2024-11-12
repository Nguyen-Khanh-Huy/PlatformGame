using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player SO", menuName = "PIS/Player SO")]
public class PlayerSO : ScriptableObject
{
    [Header("Common:")]
    public int Hp;
    public int Damage;

    [Header("Moving:")]
    public float JumpForce;
    public float SpeedMove;
    public float SpeedFly;
    public float SpeedLadder;
    public float SpeedSwim;
}