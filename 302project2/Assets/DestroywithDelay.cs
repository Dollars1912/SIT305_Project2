using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// a module class for any object which want to destroy in a time interval
/// </summary>
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
