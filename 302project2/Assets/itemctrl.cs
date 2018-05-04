using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemctrl : MonoBehaviour {
    /// <summary>
    /// control the collecting item
    /// </summary>
    public enum ItemFX
    {
        Vanish,Fly
    }
    public ItemFX itemfx;
    public float speed;
    public bool startflying;
    GameObject coinMeter;

     void Start()
    {
        startflying = false;
        if(itemfx == ItemFX.Fly)
        {
            coinMeter = GameObject.Find("money");
        }
    }
    private void Update()
    {
        if (startflying)
        {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, speed);
        }
    }
    //logic when user selct different method when collide with coin
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(itemfx == ItemFX.Vanish)
            Destroy(gameObject);
            else if(itemfx == ItemFX.Fly)
            {
                startflying = true;
                Destroy(gameObject);
            }
        }
    }
	
}
