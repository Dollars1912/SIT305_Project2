using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this class is controlling the batactivater game object. when user collide with this object, the activation will active the behavior of the flying bat to attack people. 
/// </summary>
public class batactivater : MonoBehaviour {
    public GameObject bat;
   
    flyingbatai flybat;
    
	// Use this for initialization
	void Start () {
        flybat = bat.GetComponent<flyingbatai>();
      

	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            flybat.activatebee(collision.gameObject.transform.position);
        }
    }
}
