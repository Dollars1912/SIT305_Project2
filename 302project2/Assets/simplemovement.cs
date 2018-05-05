using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplemovement : MonoBehaviour {

    public float speed;
    Rigidbody2D rb;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        SetStartdirection();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();

	}
    private void Move()
    { 
        Vector2 temp = rb.velocity;
             temp.x = speed;
             rb.velocity = temp;

       
    }
    void SetStartdirection()
    {
        if (speed > 0)
        {
            sr.flipX = true;
        }
        if (speed < 0)
        {
            sr.flipX = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("airwall")&& collision.gameObject.CompareTag("ground"))
            flipwhencollision();
    }
    void flipwhencollision()
    {
        speed = -speed;
        SetStartdirection();
    }
}
