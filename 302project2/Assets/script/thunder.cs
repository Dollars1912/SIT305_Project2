using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// make the thunderr lighting downwards, controlling the thunder object itself
/// </summary>
public class thunder : MonoBehaviour {

    public float lightspeed;
    Rigidbody2D rb;
    SpriteRenderer sr;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lighting();
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    // the movement of the sender
    public void lighting()
    {
        rb.AddForce(new Vector2(0,lightspeed));
    }
    //kill player if player collide with thunder
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
