using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// this class is controlling fire ball object in boss level it self, the controlling of the fire ball object include the direction of the dropping and the collison detection 
/// </summary>
public class fireballctrl : MonoBehaviour {
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

    // Update is called once per frame
    void Update()
    {

    }
   
    /// <summary>
    ///controlling the moving derection of the fireball.
    /// </summary>
    public void fireball()
    {
        rb.AddForce(new Vector2(movespeed, dropspeed));
        
    }
    /// <summary>
    /// this method will destroy the generated fireball object when it collide with the player and the ground
    /// The method will been called when the object fireball collided  exp(OnTriggerEnter2D(gameobject.Getcponent<Collider2D>();))
    /// it will returne a method called destrrroy to destroy the object
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("ground"))
        {
            Destroy(this.gameObject);
        }
    }
    
   
}
