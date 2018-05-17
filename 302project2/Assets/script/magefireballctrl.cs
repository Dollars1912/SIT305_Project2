using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magefireballctrl : MonoBehaviour {
    /// <summary>
    /// controlling the firreball created by the small firemage
    /// </summary>
    public float dropspeed;
    public float movespeed;
    Rigidbody2D rb;
    SpriteRenderer sr;
    
    Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        fireball();
    }



    public void fireball()
    {
        rb.AddForce(new Vector2(movespeed, dropspeed));

    }
    //Destrrroy the fireball when it collide with the player and the garabage collecter to avoid the clone firreball use all the memorry
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

       else if (collision.gameObject.CompareTag("gc"))
        {
            Destroy(this.gameObject);
        }
    }

}
