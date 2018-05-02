using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy object that contact with it
/// </summary>
public class garbagectrl : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            gamectrl.gamecontrl.playerdied(collision.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
