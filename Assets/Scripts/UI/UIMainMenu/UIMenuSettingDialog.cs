using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuSettingDialog : MonoBehaviour
{
    [SerializeField] private Button _btnSave;
    private void Start()
    {
        _btnSave.onClick.AddListener(() => UIMenuManager.Ins.Close(gameObject));
    }
}
