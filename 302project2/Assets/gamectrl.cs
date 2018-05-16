﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using DG.Tweening;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class gamectrl : MonoBehaviour {
    public static gamectrl gamecontrl;
    public float restrtdelay;
   // [HideInInspector]
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
    bool ispause;
    private HPPotCountController potCountController;

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
        potCountController = FindObjectOfType<HPPotCountController>();
    }

    // Use this for initialization
    void Start ()
    {
        timeleft = Maxtime;
        ispause = false;
        HandleFirstBoot();
        updatehealth();
        updateexp();
	}

	// Update is called once per frame
	void Update ()
    {
        if (timeleft > 0)
        {
            UpdateTimer();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            ResetData();

        if (ispause)
        {
            Time.timeScale = 0;
        }
        else if(ispause == false)
        {
            Time.timeScale = 1;
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

            ui.exp.text = data.exp_percentage.ToString() + " %";

            data.health_now = data.health_total;
            fs.Close();
        }
    }

    private void OnEnable()
    {
        Debug.Log("data loaded");
        Loaddata();
        potCountController.PotCount = data.medicine;
    }

    private void OnDisable()
    {
        Debug.Log("data saved");
        Savedata();
        Time.timeScale = 1;
    }
    public void setfirstboot(bool firstboost)
    {
        if (firstboost == true)
        {
            data.isFirstBoot = true;
        }
        else
        {
            data.isFirstBoot = false;
        }
    }
    public void ResetData()
    {

        FileStream fs = new FileStream(datafilepath, FileMode.Open,FileAccess.ReadWrite,FileShare.None);
        data.coinCount = 0;
        data.health_total = 100;
        data.health_now = data.health_total;
        data.health_percentage = 1;
        data.exp_now = 0;
        data.exp_total = 100;
        data.exp_percentage = 0;
        data.level = 1;
        timeleft = Maxtime;
        ui.txtCoinCount.text = "x 0";
        ui.exp.text = data.exp_percentage.ToString();
        ui.level.text = "Level: " + data.level.ToString();
        updatehealth();
        updateexp();
        bf.Serialize(fs, data);
        fs.Close();
    }
    public void playerdied(GameObject player)
    {
        Knight.knights.Ispowerpup = false;
        Knight.knights.isbetterrpowerup = false;
        player.SetActive(false);
        loselife();
        gainexp(value_Item.dead);
        Invoke("Gameover", restrtdelay);
        // Invoke("restartlevel", restrtdelay);
    }
    public void playerhurtanimation(GameObject player,GameObject enemy)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        playerhurt(player);
        //player.GetComponent<Collider2D>().enabled = false;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        rb.AddForce(new Vector2(-1000f, 0));
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
        Invoke("Gameover", restrtdelay);
    }
    IEnumerator pausebeforerestart(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        playerdied(player);
    }
    IEnumerator pausebeforeload(GameObject player,GameObject enemy)
    {
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>(),false);

    }
    public void firstboost(bool isfirstboost)
    {
        data.isFirstBoot = isfirstboost;

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

    void Gameover()
    {
        SceneManager.LoadScene("startPoint");
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
            ResetData();
            //set is firstboot to 0
            data.isFirstBoot = false;
        }
    }
    void updatehealth()
    {
        data.health_percentage = (data.health_now / data.health_total);

        if (data.health_percentage == 1)
        {
            ui.img_health.rectTransform.sizeDelta = new Vector2(380, ui.img_health.rectTransform.sizeDelta.y);
            ui.health_total.text = data.health_total.ToString();
            ui.health_now.text = data.health_now.ToString();
            Debug.Log(ui.img_health.rectTransform.sizeDelta + "111111");
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
        int percentage_show = Convert.ToUInt16(data.exp_percentage * 100);

        if (data.exp_percentage == 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(0, ui.img_exp.rectTransform.sizeDelta.y);
            ui.exp.text = percentage_show.ToString() + "%";

            Debug.Log(ui.img_exp.rectTransform.sizeDelta + "111111");
        }
        if (data.exp_percentage < 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(370 * (data.exp_now / data.exp_total), ui.img_exp.rectTransform.sizeDelta.y);
            ui.exp.text = percentage_show.ToString() + "%";
            Debug.Log(ui.img_exp.rectTransform.sizeDelta);
        }
        if (data.exp_percentage > 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(370 * (data.exp_now / data.exp_total - 1), ui.img_exp.rectTransform.sizeDelta.y);
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
        checkexp();
        Savedata();
        updateexp();
    }
    void checkexp()
    {
        if (data.exp_total <= data.exp_now)
        {
            data.level++;
            ui.level.text = "Level: " + data.level.ToString();
            data.exp_now = data.exp_now - data.exp_total;
            data.exp_total = data.exp_total + 20 * (data.level - 1);
            Savedata();
        }
    }

    void checkhealth()
    {
        if (data.health_now <= 0)
        {
            playerdied(Knight.knights.gameObject);
            // Debug.Log(data.exp_now);

                data.health_total = 100 + 20 * (data.level - 1);

                Savedata();
                Debug.Log("kanguole");
                Debug.Log(data.level);

            
        }
        else if (data.health_now > 0)
        {
            //data.health_total = 100 + 20 * data.level;

            Savedata();
            //Invoke("restartlevel", restrtdelay);
        }
    }

    public void IncrementPotCount()
	{
        data.medicine++;
        data.coinCount -= 100;
        ui.txtCoinCount.text = data.coinCount.ToString();

        if (data.coinCount <= 100)
        {
           // Debug.Log("I Dont have enough money");
            return;
        }
        else {
            potCountController.PotCount = data.medicine;

            Savedata();
        }

	}

	public void DecrementPotCount()
	{
        data.medicine--;
		if (data.medicine < 0)
			data.medicine = 0;

		potCountController.PotCount = data.medicine;
        Savedata();
	}

    public void UseHPPot()
    {
        DecrementPotCount();
        data.health_now += 50;
        if (data.health_now > data.health_total)
            data.health_now = data.health_total;

        updatehealth();
    }
    public void showPause()
    {
        if (ui.mobileui.activeInHierarchy)
            ui.mobileui.SetActive(false);
        ui.panelPause.SetActive(true);
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,0), 0.7f, false);
        Invoke("Setpause", 1.1f);
    }
    void Setpause()
    {
        //set the bool
        ispause = true;
    }
    public void hidePause()
    {
        ispause = false;
        if (!ui.mobileui.activeInHierarchy)
            ui.mobileui.SetActive(true);
         ui.panelPause.SetActive(false);
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 630), 0.7f, false);
       
    }
}
