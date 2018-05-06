using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Talkable : MonoBehaviour {
    public Flowchart talkFlowchart;
    public string onCollosionEnter;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Block targetBlock = talkFlowchart.FindBlock(onCollosionEnter);
            talkFlowchart.ExecuteBlock(targetBlock);
        }
    }
   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
