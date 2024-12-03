using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sp;

    [SerializeField] private Sprite openSp;
    [SerializeField] private Sprite closeSp;

    [SerializeField] private bool _isOpened;

    private void Start()
    {
        CheckSpriteDoor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CheckOpenDoor();
        }
    }
    private void CheckSpriteDoor()
    {
        _isOpened = LevelManager.Ins.levelUnlockeds[LevelManager.Ins.levelId + 1];
        _sp.sprite = _isOpened ? openSp : closeSp;
    }
    private void CheckOpenDoor()
    {
        if (_isOpened)
        {
            PlayerCtrl.Ins.PlayerCheckDoor();
            StartCoroutine(LevelPassedDialog());
            LevelManager.Ins.LevelPassedWhenOpenDoor();
            PlayerManager.Ins.key = 0;
            GameData.Ins.SaveGame();
        }
        else
        {
            if (PlayerManager.Ins.key > 0)
            {
                PlayerCtrl.Ins.PlayerCheckDoor();
                StartCoroutine(LevelPassedDialog());
                LevelManager.Ins.LevelPassedWhenCloseDoor();
                PlayerManager.Ins.key = 0;
                GameData.Ins.SaveGame();
            }
        }
    }
    private IEnumerator LevelPassedDialog()
    {
        yield return new WaitForSeconds(1.2f);
        UIGamePlayManager.Ins.Show(UIGamePlayManager.Ins.UILevelPassedDialog);
    }
}
