using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsWater : MonoBehaviour
{
    [SerializeField] private ObsController _obsController;
    private void OnTriggerStay2D(Collider2D collision)
    {
        WaterTop waterTop = collision.gameObject.GetComponent<WaterTop>();
        WaterDeep waterDeep = collision.gameObject.GetComponent<WaterDeep>();
        if (waterTop || waterTop && waterDeep)
        {
            _obsController.IsOnWaterTop = true;
            _obsController.IsOnWaterDeep = false;
            _obsController.IsOnGround = false;
        }
        if (waterDeep && _obsController.IsOnWaterTop == false)
        {
            _obsController.IsOnWaterDeep = true;
            _obsController.IsOnGround = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Ground ground = collision.gameObject.GetComponent<Ground>();
        WaterTop waterTop = collision.gameObject.GetComponent<WaterTop>();
        WaterDeep waterDeep = collision.gameObject.GetComponent<WaterDeep>();
        if (waterTop)
        {
            _obsController.IsOnWaterTop = false;
        }
        if (waterDeep)
        {
            _obsController.IsOnWaterDeep = false;
        }
        if (ground)
        {
            _obsController.IsOnGround = false;
        }

    }
}
