using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXctrl : MonoBehaviour {

    public static SFXctrl sfxcontrol;
    public GameObject sfx_item_pickup;
    private void Awake()
    {
        if (sfxcontrol == null)
            sfxcontrol = this;
    }
    /// <summary>
    /// Show sparkle effect
    /// </summary>
    /// <param name="pos"></param>
    public void ShowSparkle(Vector3 pos)
    {
        Instantiate(sfx_item_pickup, pos, Quaternion.identity);
    }
}
