using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class gamectrl : MonoBehaviour {
    public static gamectrl gamecontrl;
    public float restrtdelay;
    public gamedata data;
    public UIctrl ui;
    public float Maxtime;
    public int maxhealth;
    string datafilepath;
    
    BinaryFormatter bf;
    float timeleft;


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
       // updatehealth();
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
    void ResetData()
    {
        FileStream fs = new FileStream(datafilepath, FileMode.Create);
        data.coinCount = 0;
        ui.txtCoinCount.text = "x 0";
       
        data.health_total = 100;
        data.health_now = data.health_total;
        data.health_percentage = 1;
        data.exp_now = 0;
        data.exp_total = 100;
        data.exp_percentage = 0;
        data.level = 1;
        //updatehealth();
        updateexp();
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("has been reset");
    }

    public void playerdied(GameObject player)
    {
        player.SetActive(false);
        checkhealth();
        // Invoke("restartlevel", restrtdelay);
    }
    public void playerdied_water(GameObject player)
    {
        checkhealth();
        //Invoke("restartlevel", restrtdelay);
    }
    public void UpdateCoinCount()
    {
        data.coinCount += 1;
        ui.txtCoinCount.text = "x" + data.coinCount;
    }
    /// <summary>
    /// restart level when player dies
    /// </summary>
    void restartlevel()
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
            Debug.Log(ui.img_health.rectTransform.sizeDelta+"111111");
        }
        if (data.health_percentage < 1)
        {
            ui.img_health.rectTransform.sizeDelta = new Vector2(380 * (data.health_now / data.health_total), ui.img_health.rectTransform.sizeDelta.y);
            Debug.Log(ui.img_health.rectTransform.sizeDelta);
        }
    }
    void updateexp()
    {
        data.exp_percentage = (data.exp_now / data.exp_total);

        if (data.exp_percentage == 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(0, ui.img_exp.rectTransform.sizeDelta.y);

            Debug.Log(ui.img_exp.rectTransform.sizeDelta + "111111");
        }
        if (data.exp_percentage < 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(370 * (data.exp_now / data.exp_total), ui.img_exp.rectTransform.sizeDelta.y);
            Debug.Log(ui.img_exp.rectTransform.sizeDelta);
        }
        if(data.exp_percentage > 1)
        {
            ui.img_exp.rectTransform.sizeDelta = new Vector2(370 * (data.exp_now / data.exp_total-1), ui.img_exp.rectTransform.sizeDelta.y);
            Debug.Log(ui.img_exp.rectTransform.sizeDelta);
           
        }
    }
    void checkhealth()
    {
        Debug.Log("diaoyongle");
        //health update

        data.health_now -= 20;
        //experience update

        data.exp_now +=30;
        Debug.Log(data.exp_now);
        if (data.exp_total <= data.exp_now)
        {
            data.level++;
            data.health_total = data.health_total + 20 * data.level;
             data.exp_now = data.exp_now - data.exp_total;
            data.exp_total = data.exp_total + 20 * (data.level-1);
           
            Debug.Log("kanguole");
            Debug.Log(data.level);
        }

        if(data.health_now == 0)
        {
            data.health_now = data.health_total;
            Savedata();
           Invoke("GameOver", restrtdelay);

        }
        else
        {
          //data.health_total = 100 + 20 * data.level;

            Savedata();
            Invoke("restartlevel", restrtdelay);
        }
    }
}
