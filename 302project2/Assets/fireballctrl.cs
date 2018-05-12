using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void attk()
    {
        
    }

    /*IEnumerator releasefireball()
    {

        Enemypatrol.enmypatro.stopmoving();
        anim.SetInteger("state", 0);
        anim.SetInteger("state", 1);
        yield return new WaitForSeconds(0.5f);
   
        anim.SetInteger("state", 0);
        anim.SetInteger("state", 2);
        Debug.Log("fireball!!!1");
    }*/
    public void fireball()
    {
        rb.AddForce(new Vector2(movespeed, dropspeed));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("ground"))
        {
            Destroy(this.gameObject);
        }
    }
    
   
}
