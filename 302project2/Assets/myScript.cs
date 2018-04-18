using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myScript : MonoBehaviour {

    protected Joystick joystick;
    protected joybutton joybutton;

	// Use this for initialization
	void Start () {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<joybutton>();
	}
	
	// Update is called once per frame
	void Update () {

        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(joystick.Horizontal * 10f,
                                         rigidbody.velocity.y,
                                         joystick.Vertical * 10f);
	}
}
