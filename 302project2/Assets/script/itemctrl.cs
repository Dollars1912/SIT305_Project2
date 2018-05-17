using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemctrl : MonoBehaviour {
    /// <summary>
    /// this class is controlling the golden coin of the game
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
    /// <summary>
    /// make the efffect that coin fly to the coin count
    /// the method caled when coin collected   (exp:   always call)
    /// this will returrrn the transform of the coin flying to the coin count with a ceratin speed 
    /// </summary>
    private void Update()
    {
        if (startflying)
        {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, speed);
        }
    }
    /// <summary>
    /// contolling the status of the coin collect effect
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(itemfx == ItemFX.Vanish)
            Destroy(gameObject);
            else if(itemfx == ItemFX.Fly)
            {
                startflying = true;
                
            }
        }
    }
	
}
