using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuLevelDialog : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private UIMenuLevelPrefab _levelItemPb;
    [SerializeField] private Button _btnClose;
    private void Start()
    {
        LoadLevelItem();
        _btnClose.onClick.AddListener(() => UIMenuManager.Ins.Close(gameObject));
    }
    private void LoadLevelItem()
    {
        if (LevelManager.Ins == null || _content == null || _levelItemPb == null) return;
        for (int i = 0; i < LevelManager.Ins.levelItems.Length; i++)
        {
            int levelIdx = i;
            UIMenuLevelPrefab LevelItemPbNew = Instantiate(_levelItemPb, _content);
            _levelItemPb.SetLevelItem(LevelItemPbNew, levelIdx);
        }
    }
}
