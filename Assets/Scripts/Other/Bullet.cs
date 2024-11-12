using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speedBullet;
    [HideInInspector] public float DirBullet;
    private void Update()
    {
        transform.position += transform.right * DirBullet * _speedBullet * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Take Dame Enemy");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Take Dame Enemy");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
