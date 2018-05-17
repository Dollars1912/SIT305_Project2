using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXctrl : MonoBehaviour
{
    /// <summary>
    /// gathering all the SFX effect together forrr other class easierr to call 
    /// </summary>
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
    /// sparkle effect
    public void ShowSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_item_pickup, pos, Quaternion.identity);
    }
    public void ShowPowerupSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_powerup, pos, Quaternion.identity);
    }
    //dust effect
    public void ShowlandedSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_player_landed, pos, Quaternion.identity);
    }
    //splash effect
    public void Showsplash(Vector3 pos)
    {
        Instantiate(sfx.sfx_splash, pos, Quaternion.identity);
    }
    //explode effect
    public void enemyexplode(Vector3 pos)
    {
        Instantiate(sfx.sfx_enemy_died, pos, Quaternion.identity);
    }
}
 