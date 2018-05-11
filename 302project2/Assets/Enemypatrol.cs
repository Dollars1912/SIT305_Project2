using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// provides simpling patrolling behavior between 2 position
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftbound.position, rightbound.position);
    }
    public void move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
        anim.SetInteger("state",2);
    }
   public  void stopmoving()
    {
        
        anim.SetInteger("state", 0);
    }
    void SetStarrtingdirection()
    {
        if (speed > 0)
            
            sr.flipX = true;
        else if (speed < 0)
            sr.flipX = false;
    }
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
    void FliponEdges()
    {   
        if (sr.flipX && transform.position.x >= rightbound.position.x)
        {
           
            if (canturn)
            {
                stopmoving();
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
                 stopmoving();
                 canturn = false;
                 origanalspeed = speed;
                 speed = 0;

                 StartCoroutine(Turnright(origanalspeed));
                Debug.Log("why you not turn");

            }
        }
       
    }

    
}
