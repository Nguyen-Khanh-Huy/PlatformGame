using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGamepad : MonoBehaviour
{
    [SerializeField] private Button _btnAttack;
    [SerializeField] private Button _btnFireBullet;
    [SerializeField] private Button _btnFly;
    [SerializeField] private Button _btnJump;

    [SerializeField] private TMP_Text _txtBulletCount;
    private void Update()
    {
        _txtBulletCount.text = PlayerManager.Ins.bullet.ToString();
    }
}
