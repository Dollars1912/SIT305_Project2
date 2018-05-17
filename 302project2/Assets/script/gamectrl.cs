using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using DG.Tweening;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
/// <summary>
/// controlling the whole game behavier, this is including database, player chracter（health,exp...）,timer, pausemenu 
/// </summary>
public class gamectrl : MonoBehaviour {
    public static gamectrl gamecontrl;
    public float restrtdelay;
    [HideInInspector]
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
    //determine the file path and the classes.
    private void Awake()
    {
        if (gamecontrl == null)
            gamecontrl = this;
        bf = new BinaryFormatter();
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
    //save and load data with game.dat, the method arrre using filestream to do the create the database and read and write with database
    //the method is called anywhere when the in-game update for exp(when user get hurt, when user kill enemy, when player got kill......)  (exp: Savedata()Loaddata())
    // these method will return the value store in teh class "gamedata"   
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
    /// <summary>
    /// determine whether the game is the time playing
    /// </summary>
    /// <param name="firstboost"></param>
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
    //Initialize all the data
    public void ResetData()
    {

        FileStream fs = new FileStream(datafilepath, FileMode.Open,FileAccess.ReadWrite,FileShare.None);
        data.coinCount = 0;
        data.health_total = 100;
        data.health_now = data.health_total;
        data.medicine = 0;
        potCountController.PotCountText.text = "0";
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

    //the logic when player are died
    public void playerdied(GameObject player)
    {
        Knight.knights.Ispowerpup = false;
        Knight.knights.isbetterrpowerup = false;
        player.SetActive(false);
        loselife();
        UpdateCoinCount();
        gainexp(value_Item.dead);
        Invoke("Gameover", restrtdelay);
        // Invoke("restartlevel", restrtdelay);
    }
    // the animation called when player get hurt
    public void playerhurtanimation(GameObject player,GameObject enemy)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        //reduce health
        playerhurt(player);
        //player.GetComponent<Collider2D>().enabled = false;
        //turn off the collider between player and enemy to avoid user keep losing hurt.
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        //make user moving left when hurt by enemy
        rb.AddForce(new Vector2(-1000f, 0));
        //reload the game(turn on the collider and keep the game continue)
        StartCoroutine("pausebeforeload",player);

    }

    /// <summary>
    /// controlliing the animation when player die
    /// it will called when player die
    ///  it will returrn to the method gameover to make the user back to the startpoint
    /// </summary>
    /// <param name="player"></param>
    public void playerdiedanimation(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-3000f, 400f));
        player.GetComponent<Knight>().enabled = false;
        player.GetComponent<Collider2D>().enabled = false;

        rb.velocity = Vector2.zero;
        //disable player moving the player
        foreach(Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }
        //disable the camera which folling the player
        Camera.main.GetComponent<cameractrl>().enabled = false;
        //losing all health in 0.5s delay
        StartCoroutine("pausebeforerestart", player);
        //transfer player in the startpoint
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
    //lose health and updating the healthbar
    public void playerhurt(GameObject player)
    {
        gethurt();

        checkhealth();
        //Invoke("restartlevel", restrtdelay);
    }
    //updating the amount of the coin
    public void UpdateCoinCount()
    {
        
        data.coinCount += 10;
        ui.txtCoinCount.text = "x" + data.coinCount;
        Savedata();
    }
    /// <summary>
    /// restart level when player dies
    /// </summary>

    void Gameover()
    {
        SceneManager.LoadScene("startPoint");
    }
    //make the timer up and kill player when times up
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

    //show the particle effect when player hit the enmey
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
    //rreset the data when firrst boot
    void HandleFirstBoot()
    {
        if (data.isFirstBoot)
        {
            ResetData();
            //set is firstboot to 0
            data.isFirstBoot = false;
        }
    }
    /// <summary>
    /// controlling the health bar and the health value
    /// it will be called when playerhurt, when playerr died  (exp:updatehealth()  )
    /// the method will returrn to health value(total and cuurent) the lenghth health bar will be changing with these value
    /// </summary>
    void updatehealth()
    {
        data.health_percentage = (data.health_now / data.health_total);

        if (data.health_percentage == 1f)
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
    /// <summary>
    /// controlling the exp bar and the experience value
    /// it will be called when gain the experience (exp:updateexp())
    /// the method will returrn to experience value(total and cuurent) the lenghth exp bar will be changing with these value
    /// </summary>
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
    /// <summary>
    /// gain the exp depends on the variaty of the case
    /// the method will be called when user killing the certain enmey ,collecting the coin left by monster and died  (exp:  gainexp(value_Item.dead);)
    /// the method will return the exp value that user will gain for diffeent cases.
    /// </summary>
    /// <param name="item"></param>
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
    /// <summary>
    /// checking the experience status, upgrrade the expvalue with player level and the level up when exp is full
    /// </summary>
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
    /// <summary>
    /// checking the health status, upgrrade the health value with player level and the keeping the current health value update
    /// </summary>
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

    /// <summary>
    /// buying the medicion 
    /// the method is called whe user buy the medicine, it will reduce the coin when ther have a medicine  (exp:IncrementPotCount(); )
    /// the method will returrn the medicne count value
    /// </summary>
    public void IncrementPotCount()
	{   data.coinCount -= 100;
        data.medicine++;

        if (data.coinCount <= 100)
        {
           // Debug.Log("I Dont have enough money");
            return;
        }
        else {
            potCountController.PotCount = data.medicine;
             ui.txtCoinCount.text = data.coinCount.ToString();
            Savedata();
        }

	}
    /// <summary>
    /// controlling the usage of the medicine
    /// </summary>
	public void DecrementPotCount()
	{
        data.medicine--;
		if (data.medicine < 0)
			data.medicine = 0;

		potCountController.PotCount = data.medicine;
        Savedata();
	}
    /// <summary>
    /// increase the health when user use these medicine
    /// </summary>
    public void UseHPPot()
    {
        DecrementPotCount();
        data.health_now += 50;
        if (data.health_now > data.health_total)
            data.health_now = data.health_total;

        updatehealth();
    }
    /// <summary>
    /// show the pause panel when user click pause
    /// </summary>
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
    //hide th epause panel when user tapping continue
    public void hidePause()
    {
        ispause = false;
        if (!ui.mobileui.activeInHierarchy)
            ui.mobileui.SetActive(true);
         ui.panelPause.SetActive(false);
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 630), 0.7f, false);
       
    }
}
