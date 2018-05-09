using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ai for flying bat
/// </summary>
public class flyingbatai : MonoBehaviour {
    public float destroybeedelay = 1.4f;
    public float beespeed =2 ;
	public void activatebee(Vector3 playerpos)
    {
        transform.DOMove(playerpos, beespeed, false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("ground"))
        {
            SFXctrl.sfxcontrol.enemyexplode(collision.gameObject.transform.position);
            Destroy(gameObject, destroybeedelay);
        }
    }
}
