using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicionctrl : MonoBehaviour
{/// <summary>
/// controlling the behavier of the small firemagcion
/// </summary>

    public int health;
    SpriteRenderer sr;


    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // restore the color when hit by player
    void RestoreColor()
    {
        sr.color = Color.white;
    }
    //reduce health when player attack the firemage
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("playerattk"))
        {
            //if health is 0 the firemage will die, else the firemage will lose health
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
