using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///this class is for all the enemy with some simple AI that provides simpling patrolling behavior between 2 position
/// </summary>
public class Enemypatrol : MonoBehaviour {
    public static Enemypatrol enmypatro;
    public Transform leftbound, rightbound;
    public float speed;
    public float maxdelay,mindelay;
    bool canturn;
    float origanalspeed;
    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        SetStarrtingdirection();
        canturn = true;
	}
    private void Awake()
    {
        if (enmypatro == null)
            enmypatro = this;
     
    }
    
    // Update is called once per frame
    void Update () {
        move();
        FliponEdges();
		
	}
    /// <summary>
    /// this method can draw a line betwen two position , it will appear in the game but visible in the editting panel.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftbound.position, rightbound.position);
    }
    //controllinmg the movement of the enemy
    public void move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;

    }
//controlling the facing direction of the enemy when enemy is moving.
    void SetStarrtingdirection()
    {
        if (speed > 0)
            
            sr.flipX = true;
        else if (speed < 0)
            sr.flipX = false;
    }
    //controlling the timedelay of the movement
   IEnumerator Turnleft(float originalSpeed)
    {
        yield return new WaitForSeconds(Random.Range(mindelay, maxdelay));
        sr.flipX = false;
        speed = -originalSpeed;
        canturn = true;
    }
    IEnumerator Turnright(float orginalSpeed)
    {
        Debug.Log("turn!!!!!!!!!!");
        yield return new WaitForSeconds(Random.Range(mindelay, maxdelay));
        sr.flipX = true;
        speed = -orginalSpeed;
        canturn = true;
    }
    // make the enemy only turn around when they rreach the edge.
    void FliponEdges()
    {   
        if (sr.flipX && transform.position.x >= rightbound.position.x)
        {
           
            if (canturn)
            {
               
                canturn = false;
                origanalspeed = speed;
                speed = 0;
                StartCoroutine("Turnleft", origanalspeed);

            }
        }
        if(!sr.flipX && transform.position.x <= leftbound.position.x)
        {
            if (canturn)
            {
      
                 canturn = false;
                 origanalspeed = speed;
                 speed = 0;
                anim.SetInteger("state",1);

                 StartCoroutine(Turnright(origanalspeed));
                Debug.Log("why you not turn");

            }
        }
       
    }

    
}
