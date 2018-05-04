using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// control the event when user attact something
/// </summary>
public class attackctrl : MonoBehaviour {
   
    Rigidbody2D rib;
   
   
    public Vector2 velocity;
    public static attackctrl attackcontrl;
    void Start () {
        rib= GetComponent<Rigidbody2D>();
   
    }
    //when attcat box box dispear show open box and appear item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("box"))
        {
            Destroy(collision.gameObject.transform.gameObject);
            SFXctrl.sfxcontrol.Showopenbox(collision.gameObject.transform.position);
            
        }
    }
    //change the bullet fly speed
    public void changespeed()
    {
        velocity = new Vector2(10, 0);
        rib.velocity = velocity;
    }
}
