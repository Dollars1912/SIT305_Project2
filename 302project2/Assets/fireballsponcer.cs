using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class fireballsponcer : MonoBehaviour {

    public GameObject fireball;
    public float spawnDelay;
    public bool canSpawn,iscollide;
    Vector3 fireballrange;


    // Use this for initialization
    void Start()
    {

        canSpawn = true;
        fireballrange = new Vector3(Random.Range(-10, 137), 46, 0);
       
    }

    // Update is called once per frame
    void Update()
    {
      if (canSpawn)
        {
            changepoint();
            StartCoroutine(spawnfireball(fireballrange));
            
        }
    }
    IEnumerator spawnfireball(Vector3 fireballarange)
    {
        Instantiate(fireball, fireballarange, Quaternion.identity);
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
        Debug.Log("fireball!!!1");
    }
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
