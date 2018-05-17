using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this class is contrrolling the feet section of the player, it will recognize the attachment of player and ground and appear the dust effect when player landing
/// </summary>
public class feetctrl : MonoBehaviour {

    public Transform dustSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            SFXctrl.sfxcontrol.ShowlandedSparkle(dustSFX.position);
        }
    }
}
