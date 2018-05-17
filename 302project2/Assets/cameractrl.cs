using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this class is for the location of the camera, as the android sceen size is 1024*600 so I determine a offset at y position  
/// and also make use the position of the player to let is follow the movement of the playerr with some offset.
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
