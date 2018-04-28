using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackctrl : MonoBehaviour {
    public Vector2 velocity;
    Rigidbody2D rib;

	void Start () {
        rib= GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update()
    {
        rib.velocity = velocity;
	}
}
