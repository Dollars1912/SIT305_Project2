using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPPotCountController : MonoBehaviour {
    /// <summary>
    /// contrlling the number of the medicine
    /// </summary>
    public Text PotCountText;
    public int PotCount { get; set; }

    private void Awake()
    {
        PotCount = Convert.ToInt32(PotCountText.text); 
	}


    void FixedUpdate()
    {
        PotCountText.text = PotCount.ToString();
    }

}
