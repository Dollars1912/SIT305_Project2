using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class fireballsponcer : MonoBehaviour {

    public GameObject fireball;
    public float spawnDelay;
    public bool canSpawn;
    public static fireballsponcer fireballsnponer;
    private void Awake()
    {
        if (fireballsnponer == null)
            fireballsnponer = this;
      

    }
    // Use this for initialization
    void Start()
    {
        
        //canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void releasefireball()
    {
        canSpawn = true;
        StartCoroutine(spawnthunder());
    }
    IEnumerator spawnthunder()
    {
        
        Instantiate(fireball,new Vector3(Random.Range(-16,78),this.gameObject.transform.position.y , 0), Quaternion.identity);
        transform.DOMove(Knight.knights.gameObject.transform.position,0 , false);
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);

        canSpawn = true;
        Debug.Log("fireball!!!1");
    }
}
