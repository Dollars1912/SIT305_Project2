using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemctrl : MonoBehaviour {

    public enum ItemFX
    {
        Vanish,Fly
    }
    public ItemFX itemfx;
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(itemfx == ItemFX.Vanish)
            Destroy(gameObject);
        }
    }
	
}
