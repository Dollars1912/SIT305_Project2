using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// controlling all the behavirer of the player itself, it include(playermove,player jump, player animation,user mobileui to control the player and particle effect when event happened)
/// </summary>
public class Knight : MonoBehaviour {
    public static Knight knights;
    private void Awake()
    {
        if ( knights== null)
            knights = this;


    }

    [Tooltip("this is a the gradient for horoizontal speed")]

    public float speedBoost = 5f;
    public bool Ispowerpup,isbetterrpowerup ;
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
            attck();
        if (leftpressed)
            Horizontalmoves(-speedBoost);
        if (rightprressed)
            Horizontalmoves(speedBoost);
    }

    /// <summary>
    /// controlling the horizontal movement of the player and change the animation when user are horizontally moving
    /// the method are called when user are tapping the left and right moving button  exp(Horizontalmoves(afloat value +f))
    /// the method will return the velocity of the Rigibody2d component to move the player 
    /// </summary>
    /// <param name="playerSpeed"></param>
    void Horizontalmoves(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
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
    /// <summary>
    /// controlling the user attack behavier, it will gerate the different attack section in the left/right attack position and determine the left/right attack by the direction facing
    /// the method is called when user press attack button exp(attck())
    /// the user will return the attackctrl class by generating a acctak area in the attack position
    /// </summary>
    void attck()
    {
        
        //make the player attck in facing derection
        if (Ispowerpup == false&&isbetterrpowerup == false)
        {
          
            if (sr.flipX)
            {
                
                anim.SetInteger("state", 3);
                Instantiate(swordattkleft, swordattkleftPos.position, Quaternion.identity);
            }
            else if (!sr.flipX)
            {
                
                anim.SetInteger("state", 3);
                Instantiate(swordattkright, swordattkrightPos.position, Quaternion.identity);
            }
        }
        else if (Ispowerpup == true||isbetterrpowerup == true)
        {
            
            if (sr.flipX)
            {

                anim.SetInteger("state", 3);
                
                Instantiate(swordattkleft, swordattkleftPos.position, Quaternion.identity);
            }
            else if (!sr.flipX)
            {

                anim.SetInteger("state", 3);
                
                Instantiate(swordattkright, swordattkrightPos.position, Quaternion.identity);
            }
        }

    }
/// <summary>
/// controlling the jump behavier of the player also including double jump with animation
/// the method will called whenever the user press jump button  exp(jump())
/// the method will return to the Riginody2d component wo make the character move upwards
/// </summary>
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
    //decress the count of the medicine when tap heal button
    void Heal()
    {
        var medicineCount = gamectrl.gamecontrl.data.medicine;
        if (medicineCount > 0)
        {
            gamectrl.gamecontrl.UseHPPot();
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }
    /// <summary>
    /// this method is to determine what has been collide with user and depends on the tag of the object to do the different things
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        //determine whether collide with ground, disable jump when not at ground
        if (other.gameObject.CompareTag("ground"))
        {
            isjump = false;
        }
        //deterrrmine the collision with enemy
        if (other.gameObject.CompareTag("enemy"))
        {
            anim.SetInteger("state",-1);

            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
           
            gamectrl.gamecontrl.playerhurtanimation(gameObject,other.gameObject);
          //  yield return new WaitForSeconds(1.4f);
            anim.SetInteger("state", 1);
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(),false);
            
             
        }
        //determine the collison with monster-coin,detroy the coin, show the particle effect and gain exp 
        if (other.gameObject.CompareTag("coin_level2"))
        {
            gamectrl.gamecontrl.UpdateCoinCount();
            SFXctrl.sfxcontrol.ShowSparkle(other.gameObject.transform.position);
            Destroy(other.gameObject);
            gamectrl.gamecontrl.gainexp(gamectrl.value_Item.bigcoin);
          
        }
    }
    /// <summary>
    /// adatpt the method in mobileuictrl,make them using the method here.
    /// </summary>
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
    public void MobileHeal()
    {
        Heal();
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
    /// <summary>
    /// show the different event when user collide with object with triggered
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            //when user collect gold coin, show the effect update the coin cound and gain exp
            case "Coin":
  
                    SFXctrl.sfxcontrol.ShowSparkle(other.gameObject.transform.position);
                    gamectrl.gamecontrl.UpdateCoinCount();
                    gamectrl.gamecontrl.gainexp(gamectrl.value_Item.coin);
                
                break;
                //when user fall in water, show the splash effect and let the garbage collect to kill the player
            case "water":
               // garabagectrl.SetActive(false);
                    SFXctrl.sfxcontrol.Showsplash(other.gameObject.transform.position);
                break;
                //when user collide with enemy, call the hurtanimation to let is lose life
            case "enemy":
                // garabagectrl.SetActive(false);
                gamectrl.gamecontrl.playerhurtanimation(gameObject,other.gameObject);
                break;
            //when user collide with superrattack(thunder and fireball), call playerdiedanimation to kill player
            case "superattk":
                // garabagectrl.SetActive(false);
                gamectrl.gamecontrl.playerdiedanimation(gameObject);
                SFXctrl.sfxcontrol.enemyexplode(this.gameObject.transform.position);
                break;
                //when user pick the pwerup item call the method in attackctrl to hcanging the range and effect of the attack.
            case "powerup":
                Vector3 powerupPos = other.gameObject.transform.position;
                Destroy(other.gameObject);
                Ispowerpup = true;
                KnightModeController.knightmodectrl.currentMode = KnightModeController.KnightMode.Medium;
                SFXctrl.sfxcontrol.ShowSparkle(powerupPos);
                break;
            // //when user pick the powerup and beterpower up item call the method in attackctrl to hcanging the range and effect of the attack.
            case "betterpowerup":
                Vector3 betterpowerupPos = other.gameObject.transform.position;
                Destroy(other.gameObject);
                isbetterrpowerup= true;
                KnightModeController.knightmodectrl.currentMode = KnightModeController.KnightMode.Long;
                SFXctrl.sfxcontrol.ShowSparkle(betterpowerupPos);
                break;
      
            default:
                break;
        }
    }
}
