using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{/// <summary>
/// make the BG image score when player move
/// </summary>

	public float speed = 0.1f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{
		Vector2 offset = new Vector2(Time.time * speed, 0);

		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
