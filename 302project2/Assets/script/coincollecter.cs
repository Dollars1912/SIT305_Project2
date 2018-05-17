using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coincollecter : MonoBehaviour {
    /// <summary>
    /// this class will destroy the flying coin when collcted coin collide with the coin icon
    /// </summary>
    /// <param name="collision"></param>

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Coin"))
		{
			Destroy(collision.gameObject);
		}
	}
}


