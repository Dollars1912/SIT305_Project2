﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject Player;
    public CharacterController CharacterController;
	public Camera Camera;
	public float speed = 6.0F;
	public float jumpSpeed = 15.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (CharacterController.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;

		}
		moveDirection.y -= gravity * Time.deltaTime;
		CharacterController.Move(moveDirection * Time.deltaTime);

        var oldPos = Camera.transform.position;
        Camera.transform.position = new Vector3(Player.transform.position.x, oldPos.y, oldPos.z);
	}

    private void UpdateHorizontal()
    {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;

        Vector3 characterPos = Player.transform.position;
		Vector3 cameraPos = Camera.transform.position;

		Vector3 newCharacterPos = characterPos + new Vector3(x, 0, 0);
		Vector3 newCameraPos = cameraPos + new Vector3(x, 0, 0);

        Player.transform.position = newCharacterPos;
		Camera.transform.position = newCameraPos;
    }

}


