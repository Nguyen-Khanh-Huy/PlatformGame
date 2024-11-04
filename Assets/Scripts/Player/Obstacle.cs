using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private bool _isOnGround;
    [SerializeField] private bool _isOnLadder;
    [SerializeField] private bool _isOnWaterSurface;
    [SerializeField] private bool _isOnWaterDeep;
    [SerializeField] private Vector3 _offsetDefaul;
    [SerializeField] private Vector3 _offsetWater;

    public bool IsOnGround { get => _isOnGround; set => _isOnGround = value; }
    public bool IsOnLadder { get => _isOnLadder; set => _isOnLadder = value; }
    public bool IsOnWaterSurface { get => _isOnWaterSurface; set => _isOnWaterSurface = value; }
    public bool IsOnWaterDeep { get => _isOnWaterDeep; set => _isOnWaterDeep = value; }
    private void Update()
    {
        CheckingOffset();
    }
    private void FixedUpdate()
    {
        CheckingLayer();
    }
    private void CheckingOffset()
    {
        if(_sp.flipX == true)
        {
            if(_offsetDefaul.x < 0f && _offsetWater.x > 0f)
            {
                _offsetDefaul.x = -_offsetDefaul.x;
                _offsetWater.x = -_offsetWater.x;
            }
        }
        else
        {
            if (_offsetDefaul.x > 0f && _offsetWater.x < 0f)
            {
                _offsetDefaul.x = -_offsetDefaul.x;
                _offsetWater.x = -_offsetWater.x;
            }
        }
    }
    private void CheckingLayer()
    {
        _isOnGround = OverlapDefaul(LayerMask.GetMask("Ground")) && !OverlapWater(LayerMask.GetMask("WaterSurface")) && !OverlapWater(LayerMask.GetMask("WaterDeep"));
        _isOnLadder = OverlapDefaul(LayerMask.GetMask("Ladder"));
        _isOnWaterSurface = OverlapWater(LayerMask.GetMask("WaterSurface")) || OverlapWater(LayerMask.GetMask("WaterSurface")) && OverlapWater(LayerMask.GetMask("WaterDeep"));
        _isOnWaterDeep = !OverlapWater(LayerMask.GetMask("WaterSurface")) && OverlapWater(LayerMask.GetMask("WaterDeep"));
    }
    private bool OverlapDefaul(LayerMask layerCheck)
    {
        Collider2D Col = Physics2D.OverlapCircle(transform.position + _offsetDefaul, 0.1f, layerCheck);
        return Col != null;
    }
    private bool OverlapWater(LayerMask layerCheck)
    {
        Collider2D Col = Physics2D.OverlapCircle(transform.position + _offsetWater, 0.1f, layerCheck);
        return Col != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + _offsetDefaul, 0.1f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + _offsetWater, 0.1f);
    }
}
