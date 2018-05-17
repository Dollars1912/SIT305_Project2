using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// generate bulleted and shows the bullet fire(for this class, it will generate thunder attack) in some time interval the time interval will set in the unity project;
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
    /// <summary>
    /// the method to spown thunder with some delay
    /// The method will call when we put the thunder enmey , the enemy has this spwner to spwen the thunder. this is calling in Update() method  (exp:    StartCoroutine(spawnthunder()); )
    /// the return data of the method is a bool value canSpawn, when the bool= true the thunder appear and back to false.
    /// </summary>
    /// <returns></returns>
    IEnumerator spawnthunder()
    {
       
        Instantiate(thunder, this.gameObject.transform.position - new Vector3(0, thunder.transform.localScale.y *0.3f),Quaternion.identity);
        canSpawn = false;
    
        yield return new WaitForSeconds(spawnDelay);
        
        canSpawn = true;
        //Debug.Log("thunder!!!1");
    }
}
