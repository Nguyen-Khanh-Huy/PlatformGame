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
}
