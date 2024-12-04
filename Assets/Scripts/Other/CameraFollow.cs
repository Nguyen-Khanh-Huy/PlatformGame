using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _posX;
    [SerializeField] private float _posY;
    private void Update()
    {
        transform.position = new Vector3(PlayerCtrl.Ins.transform.position.x + _posX, PlayerCtrl.Ins.transform.position.y + _posY, transform.position.z);
    }
}
