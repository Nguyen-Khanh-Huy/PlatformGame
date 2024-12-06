using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] private Transform _bgDefault;
    [SerializeField] private Transform _bgFollow;

    [SerializeField] private GameObject _bgMoveClound;
    [SerializeField] private GameObject _bgMoveTree;
    [SerializeField] private GameObject _bgMoveGrass;

    private GameObject _cloneClound;
    private GameObject _cloneTree;
    private GameObject _cloneGrass;

    [SerializeField] private float _updatePosX;
    public float SpeedClound;
    public float SpeedTree;
    public float SpeedGrass;

    private void Start()
    {
        InsBG();
    }
    private void Update()
    {
        _bgDefault.position = new Vector2(PlayerCtrl.Ins.transform.position.x, _bgDefault.position.y);
        _bgFollow.position = PlayerCtrl.Ins.transform.position;
    }
    private void FixedUpdate()
    {
        MoveBG(_bgMoveClound, _cloneClound, SpeedClound);
        MoveBG(_bgMoveTree, _cloneTree, SpeedTree);
        MoveBG(_bgMoveGrass, _cloneGrass, SpeedGrass);
    }
    private void MoveBG(GameObject objDefault, GameObject objClone, float speed)
    {
        if(speed == SpeedTree)
        {
            speed = PlayerCtrl.Ins.Rb.velocity.x == 0f ? 0f : SpeedTree;
        }
        if (speed == SpeedGrass)
        {
            speed = PlayerCtrl.Ins.Rb.velocity.x == 0f ? 0f : SpeedGrass;
        }

        objDefault.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        objClone.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (objDefault.transform.position.x > PlayerCtrl.Ins.transform.position.x + _updatePosX)
        {
            objDefault.transform.position = new Vector2(objClone.transform.position.x - _updatePosX, objDefault.transform.position.y);
        }
        else if (objDefault.transform.position.x < PlayerCtrl.Ins.transform.position.x - _updatePosX)
        {
            objDefault.transform.position = new Vector2(objClone.transform.position.x + _updatePosX, objDefault.transform.position.y);
        }

        if (objClone.transform.position.x < PlayerCtrl.Ins.transform.position.x - _updatePosX)
        {
            objClone.transform.position = new Vector2(objDefault.transform.position.x + _updatePosX, objDefault.transform.position.y);
        }
        else if (objClone.transform.position.x > PlayerCtrl.Ins.transform.position.x + _updatePosX)
        {
            objClone.transform.position = new Vector2(objDefault.transform.position.x - _updatePosX, objDefault.transform.position.y);
        }
    }
    private void InsBG()
    {
        _cloneClound = Instantiate(_bgMoveClound, new Vector2(PlayerCtrl.Ins.transform.position.x - _updatePosX, _bgMoveClound.transform.position.y), Quaternion.identity);
        _cloneClound.transform.SetParent(GameObject.Find("BGMove").transform);

        _cloneTree = Instantiate(_bgMoveTree, new Vector2(PlayerCtrl.Ins.transform.position.x + _updatePosX, _bgMoveTree.transform.position.y), Quaternion.identity);
        _cloneTree.transform.SetParent(GameObject.Find("BGMove").transform);

        _cloneGrass = Instantiate(_bgMoveGrass, new Vector2(PlayerCtrl.Ins.transform.position.x + _updatePosX, _bgMoveGrass.transform.position.y), Quaternion.identity);
        _cloneGrass.transform.SetParent(GameObject.Find("BGMove").transform);
    }
}
