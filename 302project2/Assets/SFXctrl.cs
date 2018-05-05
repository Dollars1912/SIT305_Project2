using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXctrl : MonoBehaviour {

    public static SFXctrl sfxcontrol;
    public SFXgroup sfx; 
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
        Instantiate(sfx.sfx_item_pickup, pos, Quaternion.identity);
    }
    public void ShowPowerupSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_powerup, pos, Quaternion.identity);
    }
    public void ShowlandedSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_player_landed, pos, Quaternion.identity);
    }
    public void Showsplash(Vector3 pos)
    {
        Instantiate(sfx.sfx_splash, pos, Quaternion.identity);
    }
}
 