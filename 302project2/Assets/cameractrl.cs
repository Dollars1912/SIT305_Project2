using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// make the camera follow the player
/// </summary>
public class cameractrl : MonoBehaviour {
    public Transform player;
    public float yoffset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //make the camera follow the player in x and y axis
        transform.position = new Vector3(player.position.x, player.position.y+yoffset, transform.position.z);
	}
}
