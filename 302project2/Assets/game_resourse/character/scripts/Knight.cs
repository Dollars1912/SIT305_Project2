using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {


    [Tooltip("this is a the gradient for horoizontal speed")]

    public float speedBoost = 5f;
    public bool Ispowerpup =false;
    public float jumpspeed;
    public Transform feet;
    public float feetradius;
    public float boxwidth;
    public float boxheight;
    public float delayforDoublejump;
    public LayerMask whatIsground;
    public Transform swordattkleftPos, swordattkrightPos;
    public bool SFXison;
    //rightbulletspawnsPos, leftbulletspownsPos;
    public GameObject swordattkleft, swordattkright;
    //leftattkbullet, rightbullet
    bool isjump,canDoubleJump,isattack;
    public bool isgrounded;
    bool leftpressed, rightprressed;
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
        isgrounded = Physics2D.OverlapBox(new Vector2(feet.position.x,feet.position.y),new Vector2(boxwidth,boxheight),360.0f,whatIsground);
        float playerSpeed = Input.GetAxisRaw("Horizontalmove");
        playerSpeed *= speedBoost;
        if (playerSpeed != 0)
            Horizontalmoves(playerSpeed);
        else
            stopmoving();
        
        if (Input.GetButtonDown("Jump"))
            jump();
        if (Input.GetButtonDown("Fire1"))
        {
            attck();
            anim.SetInteger("state", 0);
        }
        if (leftpressed)
        {
            Horizontalmoves(-speedBoost);

        }
        if (rightprressed)
        {
            Horizontalmoves(speedBoost);

        }
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
    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(feet.position, new Vector3(boxwidth, boxheight, 0));
    }
    void stopmoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if(!isjump)
            anim.SetInteger("state", 0);
    }
    void attck()
    {
        //make the player attck in facing derection
        if (Ispowerpup == true)
        {
          
            if (sr.flipX)
            {
                
                anim.SetInteger("state", 3);
                Instantiate(swordattkleft, swordattkleftPos.position, Quaternion.identity);
            }
            if (!sr.flipX)
            {
                
                anim.SetInteger("state", 3);
                Instantiate(swordattkright, swordattkrightPos.position, Quaternion.identity);
            }
        }
          
    }
    void jump()
    {
        if (isgrounded) {
            isjump = true;
            rb.AddForce(new Vector2(0, jumpspeed));
            anim.SetInteger("state", 2);
            Invoke("EnableDoubleJump", delayforDoublejump);
        }
        if(canDoubleJump && !isgrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpspeed));
            anim.SetInteger("state", 2);
            canDoubleJump = false;
        }
        
    }
    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isjump = false;
        }
    }
    public void mobileleft()
    {
        leftpressed = true;

    }
    public void mobileattck()
    {
        attck();
    }
    public void mobilejump()
    {
        jump();
    }
    public void mobileright()
    {
        rightprressed = true;  
    }
    public void mobilestop()
    {
        leftpressed = false;
        rightprressed = false;
        stopmoving();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                if(SFXison)
                    SFXctrl.sfxcontrol.ShowSparkle(other.gameObject.transform.position);
                break;
            case "water":
              
                    SFXctrl.sfxcontrol.Showsplash(other.gameObject.transform.position);
                break;
            case "powerup":
                Ispowerpup = true;
                Vector3 powerupPos = other.gameObject.transform.position;
                Destroy(other.gameObject);
                if (SFXison)
                    SFXctrl.sfxcontrol.ShowSparkle(powerupPos);
                break;
            default:
                break;
        }
    }
}
