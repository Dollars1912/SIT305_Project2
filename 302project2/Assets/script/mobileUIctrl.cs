using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobileUIctrl : MonoBehaviour {
    /// <summary>
    /// controlling the mobile ui button on the scene 
    /// each of them well been called when user tap the button (exp： mobileMoveleft（）;  mobileMoverright（);mobileMovestop();mobileMoveattck();mobileMovejump();mobildeHeal();
    /// methods will return the method in the player controll class to action the button
    /// </summary>
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
        player.MobileHeal();   
    }

}
