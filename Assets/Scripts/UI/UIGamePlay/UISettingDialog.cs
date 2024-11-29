using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISettingDialog : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    [SerializeField] private Button _btnSave;

    private void Start()
    {
        _btnSave.onClick.AddListener(() => UIGamePlayManager.Ins.Close(gameObject));
    }
}
