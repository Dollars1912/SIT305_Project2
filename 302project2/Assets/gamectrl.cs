using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class gamectrl : MonoBehaviour {
    public static gamectrl gamecontrl;
    public float restrtdelay;
    public gamedata data;
    string datafilepath;
    BinaryFormatter bf;


    private void Awake()
    {
        if (gamecontrl == null)
            gamecontrl = this;
        bf = new BinaryFormatter();
        datafilepath = "C:/Users/97290/Desktop/新建文件夹/SIT305_Project2/data/game.dat";
        Debug.Log(datafilepath);
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ResetData();
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
        bf.Serialize(fs, data);
        fs.Close();
    }

    public void playerdied(GameObject player)
    {
        player.SetActive(false);
        
        Invoke("restartlevel", restrtdelay);
    }
    public void playerdied_water(GameObject player)
    {
        Invoke("restartlevel", restrtdelay);
    }
    /// <summary>
    /// restart level when player dies
    /// </summary>
    void restartlevel()
    {
        SceneManager.LoadScene("startPoint");
    }
}
