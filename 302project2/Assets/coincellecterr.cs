using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coincellecterr : MonoBehaviour {
    /// <summary>
    /// logic for coin
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collector"))
        {
            Destroy(collision.gameObject);
        }
    }
}
