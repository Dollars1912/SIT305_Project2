﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobileUIctrl : MonoBehaviour {

    public GameObject knight;
    Knight player;
	// Use this for initialization
	void Start () {
        player = knight.GetComponent<Knight>();
		
	}
	public void mobileMoveleft()
    {
        player.mobileleft();
    }
    public void mobileMoverright()
    {
        player.mobileright();
    }
    public void mobileMovestop()
    {
        player.mobilestop();
    }
    public void mobileMoveattck()
    {
        player.mobileattck();
    }
    public void mobileMovejump()
    {
        player.mobilejump();
    }
    public void mobildeHeal()
    {
        //TODO
    }
    // Update is called once per frame
    void Update () {
		
	}
}
