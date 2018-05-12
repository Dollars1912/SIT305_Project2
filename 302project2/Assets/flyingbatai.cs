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
    public int health;
    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

    }
    void RestoreColor()
    {
        sr.color = Color.white;
    }
    public void activatebee(Vector3 playerpos)
    {
        transform.DOMove(playerpos, beespeed, false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SFXctrl.sfxcontrol.enemyexplode(collision.gameObject.transform.position);
            Destroy(gameObject, destroybeedelay);
        }
        else  if (collision.gameObject.CompareTag("playerattk"))
        {
            if (health == 0)
                gamectrl.gamecontrl.hitenemy(gameObject.transform);
            if (health > 0)
            {
                health --;
                sr.color = Color.red;
                Invoke("RestoreColor", 0.1f);
            }
        }
    }
}
