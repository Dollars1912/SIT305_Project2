using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicionctrl : MonoBehaviour
{

    public int health;
    SpriteRenderer sr;


    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    void RestoreColor()
    {
        sr.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("playerattk"))
        {
            if (health == 0)
                gamectrl.gamecontrl.hitenemy(gameObject.transform);
            if (health > 0)
            {
                health--;
                sr.color = Color.red;
                Invoke("RestoreColor", 0.1f);
            }
        }
    }

}
