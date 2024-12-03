using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamepad : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtBulletCount;

    private void Update()
    {
        _txtBulletCount.text = PlayerManager.Ins.bullet.ToString();
    }
}
