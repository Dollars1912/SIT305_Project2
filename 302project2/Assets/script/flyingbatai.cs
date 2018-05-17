using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ai for flying bat controlling the attack behavier and the object it self
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
    // the method is called when the bat been attack, it will change color to make userr easier to recognized.
    void RestoreColor()
    {
        sr.color = Color.white;
    }
    /// <summary>
    /// the method is to making the flying bat to attack people, the flying bat will run to the player position when player reach the activaterr zone.
    /// this method is been called when player reach the bat activater collider area  （exp:   activatebee( knight.knights.gameoject.Transform.position)
    /// the method is return to the playerposition and flying in a certain spped.
    /// </summary>
    /// <param name="playerpos"></param>
    public void activatebee(Vector3 playerpos)
    {
        transform.DOMove(playerpos, beespeed, false);
    }
    //when bat collide with player, it will directly kill player and bat it self, when player hit the bat it will change color and reduce health
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
