using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coincellecterr : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collector"))
        {
            Destroy(collision.gameObject);
        }
    }
}
