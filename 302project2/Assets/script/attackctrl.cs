using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// the contrrol script of the attack section
/// when user press attack button it will generate a attack section.
/// </summary>
public class attackctrl : MonoBehaviour {

    public Sprite newimage;
    Rigidbody2D rib; 
    Vector2 velocity;
    SpriteRenderer sr;
    public static attackctrl attackcontrl;
    private void Awake()
    {
        if (attackcontrl == null)
            attackcontrl= this;

       
    }
    void Start () {
        rib= GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        changespeed();
    }
    private void Update()
    {
        rib.velocity = velocity;

    }
    /// <summary>
    /// validate the collition between the user attack and the object. when attack section collide with box and enmemy, the event triggered.
    /// The user will be callen two trigged cdollision data are collide .  (exp: OnTriggerEnter2D(gameobject.getComponent<Collider2D>();) )
    /// returm snippet: 1. when the attack section collide with box, it will return a method from box controll call" boxController.OnTriggered", this method is to control the item appear when user 
    /// open the box
    /// 2. when user attack the object contain enemy tag, the method is return a method Destroy() to destroy the collide object.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("box"))
        {
            var boxController = collision.gameObject.GetComponent<BoxContoller>();
            if (!boxController)
                return;
            
            boxController.OnTriggered();
        }
        else if (collision.gameObject.CompareTag("enemy"))
        {
           
            Destroy(gameObject);
        }
      
    }
    //this method is to change the movement of the attack section. 
    //the method will called when user collect item. (exp: changespeed())
    //it will returned the a value of the movement spped and then destrtoy the attactk section in a time interval. 
    public void changespeed()
    { 
        if(Knight.knights.Ispowerpup == true&& Knight.knights.isbetterrpowerup == false && Knight.knights.GetComponent<SpriteRenderer>().flipX==true)
        {
            velocity = new Vector2(-10,0);
            Destroy(this.gameObject, 1f);
        }
        else  if (Knight.knights.Ispowerpup == true && Knight.knights.isbetterrpowerup == false && Knight.knights.GetComponent<SpriteRenderer>().flipX == false)
        {
                velocity = new Vector2(10, 0);
            Destroy(this.gameObject, 1f);
        }
        if (Knight.knights.Ispowerpup == true && Knight.knights.isbetterrpowerup == true && Knight.knights.GetComponent<SpriteRenderer>().flipX == true)
        {
            sr.sprite = newimage;
            velocity = new Vector2(-17, 0);
            Destroy(this.gameObject, 2f);
        }
        else if (Knight.knights.Ispowerpup == true && Knight.knights.isbetterrpowerup == true && Knight.knights.GetComponent<SpriteRenderer>().flipX == false)
        {
            sr.sprite = newimage;
            velocity = new Vector2(17, 0);
            Destroy(this.gameObject, 2f);
        }
        else if (Knight.knights.Ispowerpup == false)
        {
            Destroy(this.gameObject, 0.2f);
        }
        Debug.Log("levelup");
    }
}
