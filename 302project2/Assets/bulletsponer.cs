using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// generate bulleted and shows the bullet fire in some time interval;
/// </summary>
public class bulletsponer : MonoBehaviour {
    public GameObject thunder;
    public float spawnDelay;
    public bool canSpawn;

    // Use this for initialization
    void Start () {
        canSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (canSpawn)
        {
            
            StartCoroutine(spawnthunder());
           
        }
	}
    void DestroywithDelay()
    {
         Destroy(thunder,spawnDelay);
        Debug.Log("thunder destroyed");
    }
    IEnumerator spawnthunder()
    {
       
        Instantiate(thunder, this.gameObject.transform.position - new Vector3(0, thunder.transform.localScale.y *0.3f),Quaternion.identity);
        canSpawn = false;
    
        yield return new WaitForSeconds(spawnDelay);
        
        canSpawn = true;
        Debug.Log("thunder!!!1");
    }
}
