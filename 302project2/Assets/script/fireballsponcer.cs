using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// this class is contrrolling the sponer of the fireball(genrate fireball in the suit time and position)
/// </summary>
public class fireballsponcer : MonoBehaviour {

    public GameObject fireball;
    public float spawnDelay;
    public bool canSpawn,iscollide;
    Vector3 fireballrange;


    // Use this for initialization
    void Start()
    {
        //spawn the fireball in the random range by default
        canSpawn = true;
        fireballrange = new Vector3(Random.Range(-10, 137), 46, 0);
       
    }

    // Update is called once per frame
    void Update()
    {
        //
      if (canSpawn)
        {
            changepoint();
            StartCoroutine(spawnfireball(fireballrange));                                                                                                                            
            
        }
    }
    //release the fire ball in a certain delays
    IEnumerator spawnfireball(Vector3 fireballarange)
    {
        Instantiate(fireball, fireballarange, Quaternion.identity);
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
        Debug.Log("fireball!!!1");
    }
    /// <summary>
    /// this method is changing the position when fireball generrrated
    /// it will been called when user attach a certain area near the firemage. when user are far away frrom the mage, it will attack in large range, but when close to him, he will shoot the fire ball near the magicion to protect himself
    ///  exp(  changepoint())
    /// the method is returned in a range of a position in the map.
    /// </summary>
    void changepoint()
    {
        if (iscollide == true)
        { fireballrange = new Vector3(Random.Range(33, 57), 46, 0);
        }
        else if (iscollide == false)
        {
            fireballrange = new Vector3(Random.Range(-10, 137), 46, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            iscollide = true;
        }
        else if((collision.gameObject.CompareTag("Player")==false))
        {
            iscollide = false;
        }
      
    }
}
