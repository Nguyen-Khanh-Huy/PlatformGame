using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2 dirKnockBack = (collision.transform.position - transform.position).normalized;
            PlayerCtrl.Ins.TakeDamagePlayer(1, dirKnockBack);
        }
    }
}
