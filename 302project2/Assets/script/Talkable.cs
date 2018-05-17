using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
/// <summary>
/// flow chart controlling class to detect the collison of the NPC and player
/// </summary>
public class Talkable : MonoBehaviour {
    public Flowchart talkFlowchart;
    public string onCollosionEnter;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {//when player attach NPC it will targeting the block in flowchart to start the dialog
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
