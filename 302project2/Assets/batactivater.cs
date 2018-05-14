using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
