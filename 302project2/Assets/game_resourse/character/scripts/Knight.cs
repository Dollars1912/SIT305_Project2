using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

    public Rigidbody2D rigidBody;
	private Vector2 move = Vector2.zero;

	public float verticalTopSpeed = 2f;
	public float horizontalTopSpeed = 8f;
    public float gravity = 20f;
    public float jumpSpeed = 5f; 

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move = new Vector2(Input.GetAxis("Horizontal"), 0);
		if (Input.GetButton("Jump"))
			move.y = jumpSpeed;

        move = new Vector2(move.x * horizontalTopSpeed, move.y * verticalTopSpeed);
        move.y -= gravity * Time.deltaTime;
        var currentVelocity = rigidBody.velocity;

        rigidBody.velocity = new Vector2(move.x, move.y);
   	}   
}
