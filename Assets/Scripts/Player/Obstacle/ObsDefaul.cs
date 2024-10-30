using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ObsDefaul : MonoBehaviour
{
    [SerializeField] private ObsController _obsController;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Ground ground = collision.gameObject.GetComponent<Ground>();
        WaterTop waterTop = collision.gameObject.GetComponent<WaterTop>();
        WaterDeep waterDeep = collision.gameObject.GetComponent<WaterDeep>();
        Ladder ladder = collision.gameObject.GetComponent<Ladder>();
        if (ground && _obsController.IsOnWaterTop == false && _obsController.IsOnWaterDeep == false)
        {
            _obsController.IsOnGround = true;
        }
        if (waterTop || waterTop && waterDeep)
        {
            _obsController.IsOnGround = false;
            _obsController.IsOnWaterTop = true;
        }
        if (waterDeep && _obsController.IsOnWaterTop == false)
        {
            _obsController.IsOnGround = false;
        }
        if (ladder)
        {
            _obsController.IsOnLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Ground ground = collision.gameObject.GetComponent<Ground>();
        WaterTop waterTop = collision.gameObject.GetComponent<WaterTop>();
        WaterDeep waterDeep = collision.gameObject.GetComponent<WaterDeep>();
        Ladder ladder = collision.gameObject.GetComponent<Ladder>();
        if (ground)
        {
            _obsController.IsOnGround = false;
        }
        if (waterTop)
        {
            _obsController.IsOnWaterTop = false;
        }
        if (ladder)
        {
            _obsController.IsOnLadder = false;
        }
    }
}
