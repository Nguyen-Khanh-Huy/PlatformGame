using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pref : MonoBehaviour
{
    public static string GameData
    {
        set => PlayerPrefs.SetString(GamePref.GameData.ToString(), value);
        get => PlayerPrefs.GetString(GamePref.GameData.ToString());
    }
    public static bool FirstTime
    {
        get => PlayerPrefs.GetInt(GamePref.FirstTime.ToString(), 1) == 1;
        set => PlayerPrefs.SetInt(GamePref.FirstTime.ToString(), value ? 1 : 0);
    }

    //public static void SetBool(string key, bool isOn)
    //{
    //    if (isOn)
    //    { PlayerPrefs.SetInt(key, 1); }
    //    else
    //    { PlayerPrefs.SetInt(key, 0); }
    //}

    //public static bool GetBool(string key, bool defaultValue = false)
    //{
    //    return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) == 1 ? true : false : defaultValue;
    //}
}
