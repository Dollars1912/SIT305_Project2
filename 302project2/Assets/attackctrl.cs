using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackctrl : MonoBehaviour {
   
    Rigidbody2D rib;
   
   
    public Vector2 velocity;
    public static attackctrl attackcontrl;
    void Start () {
        rib= GetComponent<Rigidbody2D>();
   
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
    }
    public void changespeed()
    {
        velocity = new Vector2(10, 0);  
        rib.velocity = velocity;
    }
}
