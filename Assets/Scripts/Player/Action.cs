using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private Vector3 _offset;
    private void Update()
    {
        CheckingOffset();
    }
    private void CheckingOffset()
    {
        if (_sp.flipX == true)
        {
            if (_offset.x > 0f)
            {
                _offset.x = -_offset.x;
            }
        }
        else
        {
            if (_offset.x < 0f)
            {
                _offset.x = -_offset.x;
            }
        }
    }
    private void Attack()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position + _offset, 0.6f, LayerMask.GetMask("Enemy"));
        if(col != null)
        {
            Debug.Log("Tacke Dame Enemy");
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position + _offset, 0.6f);
    //}
}
