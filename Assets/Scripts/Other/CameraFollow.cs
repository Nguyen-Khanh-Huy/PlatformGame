using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}
