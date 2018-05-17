using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openboxctrl : MonoBehaviour {
    /// <summary>
    /// controlling the behavier of the openbox(when user open the box), destroy it will certain delay
    /// </summary>
    public float destroyDelay=2f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyDelay);
    }
}
