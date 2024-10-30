using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _ColDefaul;
    [SerializeField] private CircleCollider2D _ColWater;

    [SerializeField] private bool _isOnGround;
    [SerializeField] private bool _isOnLadder;
    [SerializeField] private bool _isOnWaterTop;
    [SerializeField] private bool _isOnWaterDeep;

    public bool IsOnGround { get => _isOnGround; set => _isOnGround = value; }
    public bool IsOnLadder { get => _isOnLadder; set => _isOnLadder = value; }
    public bool IsOnWaterTop { get => _isOnWaterTop; set => _isOnWaterTop = value; }
    public bool IsOnWaterDeep { get => _isOnWaterDeep; set => _isOnWaterDeep = value; }
}
