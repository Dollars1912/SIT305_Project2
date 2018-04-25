using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

    
    [Tooltip("this is a the grradient for horoizontal speed")]
    public float speedBoost = 5f;
    public float jumpspeed;
    bool isjump;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float playerSpeed = Input.GetAxisRaw("Horizontalmove");
       playerSpeed *= speedBoost;
        if (playerSpeed != 0)
            Horizontalmoves(playerSpeed);
        else
            stopmoving();
        
        if (Input.GetButtonDown("Jump"))
            jump();
   	}   


    void Horizontalmoves(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        print(rb.velocity);
        if (playerSpeed < 0)
            sr.flipX = true;
        else if (playerSpeed > 0)
            sr.flipX = false;
        if(!isjump)
            anim.SetInteger("state", 1);
    }
    void stopmoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if(!isjump)
            anim.SetInteger("state", 0);
    }
    void jump()
    {
        rb.AddForce(new Vector2(0, jumpspeed));
        anim.SetInteger("state", 2);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isjump = false;
        }
    }
}
