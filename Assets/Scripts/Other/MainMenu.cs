using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (Pref.FirstTime)
        {
            Debug.Log("Lan 1");
            GameData.Ins.SaveGame();
        }
        else
        {
            Debug.Log("Lan 2,3,4,5,...");
            GameData.Ins.LoadGame();
        }
        Pref.FirstTime = false;
    }
}
