using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackctrl : MonoBehaviour {
 
    Rigidbody2D rib; 
   Vector2 velocity;
    public static attackctrl attackcontrl;
    private void Awake()
    {
        if (attackcontrl == null)
            attackcontrl= this;

       
    }
    void Start () {
        rib= GetComponent<Rigidbody2D>();
        changespeed();
    }
    private void Update()
    {
        rib.velocity = velocity;

    }
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
            gamectrl.gamecontrl.hitenemy(collision.gameObject.transform);
            Destroy(gameObject);
        }
      
    }

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
            velocity = new Vector2(-17, 0);
            Destroy(this.gameObject, 2f);
        }
        else if (Knight.knights.Ispowerpup == true && Knight.knights.isbetterrpowerup == true && Knight.knights.GetComponent<SpriteRenderer>().flipX == false)
        {
            velocity = new Vector2(17, 0);
            Destroy(this.gameObject, 2f);
        }
        else if (Knight.knights.Ispowerpup == false)
        {
            Destroy(this.gameObject, 0.5f);
        }





        Debug.Log("levelup");
    }
}
