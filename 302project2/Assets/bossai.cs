using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// this class is controlling the behavior ai(move,attack,die) of the boss object
/// </summary>
public class bossai : MonoBehaviour {
    public float jumpspeed;
    public int startjumpat;
    public int jumpdelay;
    public int health;
    public Slider bosshealth;
    public float boss_speed;
    public GameObject bossfireball;
    public float delayforfiring;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector3 bulleySpwonsPos;
    bool canfire, isjumping,isdead;
    public GameObject enemy;


	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        canfire = false;
        bulleySpwonsPos = gameObject.transform.position;
        Invoke("reload", Random.Range(1f, delayforfiring));
	}
	
	// Update is called once per frame
	void Update () {
        move();
        if (canfire)
        {
            Fireballcoming();
            canfire = false;
        }
	}
    void reload()
    {
        canfire = true;

    }
    void RestoreColor()
    {
        sr.color = Color.white;
    }
     void move()
    {
        float speed = boss_speed = Random.Range(-8,5);
        Vector2 temp = rb.velocity;
        temp.x = speed;
        temp.y = speed;
        rb.velocity = temp;
 
    }
    void Fireballcoming()
    {
        Instantiate(bossfireball, new Vector3(Random.Range(-6, 33), 5.701f, 0), Quaternion.identity);
        Invoke("reload", delayforfiring);
    }
    void bossdied()
    {
        if (health <= 0)
        {
            isdead = true;
            Destroy(enemy,3);
            Instantiate(bossfireball,this.transform.position, Quaternion.identity);
            SceneManager.LoadScene("game");
           

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)

    {
    if (collision.gameObject.CompareTag("playerattk"))
    {
        if (health == 0)
            gamectrl.gamecontrl.hitenemy(gameObject.transform);
        if (health > 0)
        {
            health = health - 20;
            bosshealth.value = (float)health;
                sr.color= Color.red;
                Invoke("RestoreColor",0.1f);
        }
    }
    }
}
