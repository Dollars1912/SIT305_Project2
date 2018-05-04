using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openboxctrl : MonoBehaviour {
    /// <summary>
    /// contrrol the open box(after attack box)
    /// </summary>
    public float destroyDelay=2f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyDelay);
    }
}
