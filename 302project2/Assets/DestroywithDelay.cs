using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroywithDelay : MonoBehaviour {
    public float delay;//set to 1.5
    
	// Use this for initialization
	void Start () {
        Destroy(gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
