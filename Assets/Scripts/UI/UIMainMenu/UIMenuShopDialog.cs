using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuShopDialog : MonoBehaviour
{
    [SerializeField] private Button _btnBullet;
    [SerializeField] private Button _btnHp;
    [SerializeField] private Button _btnLife;
    [SerializeField] private Button _btnKey;

    [SerializeField] private TMP_Text _txtBulletPrice;
    [SerializeField] private TMP_Text _txtHpPrice;
    [SerializeField] private TMP_Text _txtLifePrice;
    [SerializeField] private TMP_Text _txtKeyPrice;

    [SerializeField] private TMP_Text _txtBulletCount;
    [SerializeField] private TMP_Text _txtHpCount;
    [SerializeField] private TMP_Text _txtLifeCount;
    [SerializeField] private TMP_Text _txtKeyCount;

    [SerializeField] private int _bulletPrice;
    [SerializeField] private int _hpPrice;
    [SerializeField] private int _lifePrice;
    [SerializeField] private int _keyPrice;

    [SerializeField] private TMP_Text _txtCoin;
    [SerializeField] private Button _btnClose;
    private void Start()
    {
        _txtBulletPrice.text = _bulletPrice.ToString();
        _txtHpPrice.text = _hpPrice.ToString();
        _txtLifePrice.text = _lifePrice.ToString();
        _txtKeyPrice.text = _keyPrice.ToString();

        _btnBullet.onClick.AddListener(() => ButtonAction(PlayerManager.Ins.bullet, _bulletPrice));
        _btnHp.onClick.AddListener(() => ButtonAction(PlayerManager.Ins.hp, _hpPrice));
        _btnLife.onClick.AddListener(() => ButtonAction(PlayerManager.Ins.life, _lifePrice));
        _btnKey.onClick.AddListener(() => ButtonAction(PlayerManager.Ins.key, _keyPrice));

        _btnClose.onClick.AddListener(() => UIMenuManager.Ins.Close(gameObject));
    }
    private void Update()
    {
        _txtBulletCount.text = PlayerManager.Ins.bullet.ToString();
        _txtHpCount.text = PlayerManager.Ins.hp.ToString();
        _txtLifeCount.text = PlayerManager.Ins.life.ToString();
        _txtKeyCount.text = PlayerManager.Ins.key.ToString();

        _txtCoin.text = PlayerManager.Ins.coin.ToString();
    }
    private void ButtonAction(int itemData, int price)
    {
        if(PlayerManager.Ins.coin >= price)
        {
            itemData++;
            PlayerManager.Ins.coin -= price;
            GameData.Ins.SaveGame();
        }
        else
        {
            Debug.Log("You dont have enough Coin !!!");
        }
    }
}
