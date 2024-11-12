using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy SO", menuName = "PIS/Enemy SO")]
public class EnemySO : ScriptableObject
{
    [Header("Common:")]
    public int Hp;
    public int Damage;

    [Header("Moving:")]
    public float SpeedMove;
    public float SpeedChasing;

    [Header("Distance:")]
    public float MovingDist;
}
