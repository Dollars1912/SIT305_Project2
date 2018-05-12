using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class gamectrl : MonoBehaviour {
    public static gamectrl gamecontrl;
    public float restrtdelay;
    public gamedata data;
    public UIctrl ui;
    public GameObject bigcoin;
    public int coinvalue;
    public float Maxtime;
    public int maxhealth;
    public int bigcoinvalue;
    public int enemyvalue;
    string datafilepath;
    
    BinaryFormatter bf;
    float timeleft;
    public enum value_Item
    {
        coin,bigcoin,enemy1,enemy2,enemy3,dead
    }

    private void Awake()
    {
        if (gamecontrl == null)
            gamecontrl = this;
        bf = new BinaryFormatter();
        //C:/Users/97290/Desktop/新建文件夹/SIT305_Project2
        datafilepath = "../data/game.dat";
        
    }
    // Use this for initialization
    void Start ()
    {
        timeleft = Maxtime;
        HandleFirstBoot();
        updatehealth();
        updateexp();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ResetData();
        if (timeleft > 0)
        {
            UpdateTimer();
        }
	}
    public void Savedata()
    {
        FileStream fs = new FileStream(datafilepath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
    }
    public void Loaddata()
    {
        if (File.Exists(datafilepath))
        {
            FileStream fs = new FileStream(datafilepath, FileMode.Open);
            data = (gamedata)bf.Deserialize(fs);
            ui.txtCoinCount.text = "x" + data.coinCount;
            ui.level.text = "Level: " + data.level.ToString();
            data.health_now = data.health_total;
        
            fs.Close();
        }
    }
    private void OnEnable()
    {
        Debug.Log("data loaded");
        Loaddata();
    }
    private void OnDisable()
    {
        Debug.Log("data saved");
        Savedata();
    }
    public void ResetData()
    {
        FileStream fs = new FileStream(datafilepath, FileMode.Create);
        data.coinCount = 0;
        ui.txtCoinCount.text = "x 0";
        ui.exp.text = data.exp_percentage.ToString();
        data.health_total = 100;
        data.health_now = data.health_total;
        data.health_percentage = 1;
        data.exp_now = 0;
        data.exp_total = 100;
        data.exp_percentage = 0;
        data.level = 1;
        updatehealth();
        updateexp();
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("has been reset");
    }

    public void playerdied(GameObject player)
    {
        player.SetActive(false);
        loselife();
        gainexp(value_Item.dead);
        checkhealth();
        // Invoke("restartlevel", restrtdelay);
    }
    public void playerhurtanimation(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-3000f, 400f));
        player.GetComponent<Knight>().enabled = false;
        ///player.GetComponent<Collider2D>().enabled = false;
   
       // rb.velocity = Vector2.zero;
        /*foreach(Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }
        Camera.main.GetComponent<cameractrl>().enabled = false;*/
        StartCoroutine("pausebeforeload",player);
      
    }
    public void playerdiedanimation(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-3000f, 400f));
        player.GetComponent<Knight>().enabled = false;
        player.GetComponent<Collider2D>().enabled = false;

        rb.velocity = Vector2.zero;
        foreach(Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }
        Camera.main.GetComponent<cameractrl>().enabled = false;
        StartCoroutine("pausebeforerestart", player);

    }
    IEnumerator pausebeforerestart(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        playerdied(player);
    }
    IEnumerator pausebeforeload(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Knight>().enabled = true;
        playerhurt(player);
    }
    public void playerhurt(GameObject player)
    {
        gethurt();
        
        checkhealth();
        //Invoke("restartlevel", restrtdelay);
    }
    public void UpdateCoinCount()
    {
        Savedata();
        data.coinCount += 10;
        ui.txtCoinCount.text = "x" + data.coinCount;
    }
    /// <summary>
    /// restart level when player dies
    /// </summary>
    void restartlevel()
    {
        SceneManager.LoadScene("level2");
    }
    void Gameover()
    {
        SceneManager.LoadScene("level2");
    }
    void UpdateTimer()
    {
        timeleft -= Time.deltaTime;
        ui.txtTimer.text = "Timer:" + (int)timeleft;
        if (timeleft <= 0)
        {
            ui.txtTimer.text = "dead is coming!";
            GameObject player = GameObject.FindGameObjectWithTag("Player") as GameObject;
            playerdied(player);
        }
    }
    public void hitenemy(Transform enemy)
    {
        //show the explosion
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXctrl.sfxcontrol.enemyexplode(pos);
        //Destrroy enemy
        Destroy(enemy.gameObject);
        //show the item
        Instantiate(bigcoin,pos,Quaternion.identity);
    }
    void HandleFirstBoot()
    {
        if (data.isFirstBoot)
        {
            //set health
            data.health_total = 120;
            data.health_now = data.health_total;
            data.health_percentage = 1;
            //set experience
            data.exp_total = 100;
            data.exp_now = 0;
            data.exp_percentage = 0;
            //set level
            data.level = 1;
            //set number of coins to 0 
            data.coinCount = 0;
            //set exp to 0
            data.exp_now = 0;
            //set is firstboot to 0
            data.isFirstBoot = false;
        }
    }
   void updatehealth()
    {
        data.health_percentage = (data.health_now / data.health_total );

        if (data.health_percentage==1)
        {
            ui.img_health.rectTransform.sizeDelta = new Vector2(380, ui.img_health.rectTransform.sizeDelta.y);
            ui.health_total.text = data.health_total.ToString();
            ui.health_now.text = data.health_now.ToString();
            Debug.Log(ui.img_health.rectTransform.sizeDelta+"111111");
        }
        if (data.health_percentage < 1)
        {
            ui.img_health.rectTransform.sizeDelta = new Vector2(380 * (data.health_now / data.health_total), ui.img_health.rectTransform.sizeDelta.y);
            ui.health_total.text = data.health_total.ToString();
            ui.health_now.text = data.health_now.ToString();
            Debug.Log(ui.img_health.rectTransform.sizeDelta);
        }
    }
    void updateexp()
    {
        data.exp_percentage = (data.exp_now / data.exp_total);
        int percentage_show =Convert.ToUInt16(data.exp_percentage * 100);

        if (data.exp_percentage == 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(0, ui.img_exp.rectTransform.sizeDelta.y);
            ui.exp.text = percentage_show.ToString()+ "%";

            Debug.Log(ui.img_exp.rectTransform.sizeDelta + "111111");
        }
        if (data.exp_percentage < 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(370 * (data.exp_now / data.exp_total), ui.img_exp.rectTransform.sizeDelta.y);
            ui.exp.text = percentage_show.ToString() + "%";
            Debug.Log(ui.img_exp.rectTransform.sizeDelta);
        }
        if(data.exp_percentage > 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(370 * (data.exp_now / data.exp_total-1), ui.img_exp.rectTransform.sizeDelta.y);
            ui.exp.text = percentage_show.ToString() + "%";
            Debug.Log(ui.img_exp.rectTransform.sizeDelta);
           
        }
    }
    void loselife()
    {
        data.health_now = 0;
        Savedata();
        
    }
    void gethurt()
    {
        data.health_now -= 20;
        Savedata();
        updatehealth();
    }
    public void gainexp(value_Item item)
    {
        int itmvalue = 0;
        switch (item)
        {
            case value_Item.bigcoin:
                itmvalue = bigcoinvalue;
                break;
            case value_Item.enemy1:
                itmvalue = enemyvalue;
                break;
            case value_Item.dead:
                itmvalue = 50;
                break;
            default:
                break;
        }
        data.exp_now += itmvalue;
        Savedata();
        updateexp();
    }

    void checkhealth()
    {
        if(data.health_now <= 0)
        {
            
           // Debug.Log(data.exp_now);
            if (data.exp_total <= data.exp_now)
            {

                data.level++;
               
                ui.level.text ="Level: "+ data.level.ToString();
                data.health_total = 100 + 20 * (data.level-1);
                data.exp_now = data.exp_now - data.exp_total;
                data.exp_total = data.exp_total + 20 * (data.level - 1);
                Savedata();
                Debug.Log("kanguole");
                Debug.Log(data.level);
                Savedata();
            }
           
            
            Invoke("Gameover", restrtdelay);
        }
        else if(data.health_now > 0)
        {
          //data.health_total = 100 + 20 * data.level;

            Savedata();
            //Invoke("restartlevel", restrtdelay);
        }
    }
}
