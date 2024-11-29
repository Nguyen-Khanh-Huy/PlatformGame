using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuShopDialog : MonoBehaviour
{
    [SerializeField] private Button _btnClose;
    private void Start()
    {
        _btnClose.onClick.AddListener(() => UIMenuManager.Ins.Close(gameObject));
    }
}
