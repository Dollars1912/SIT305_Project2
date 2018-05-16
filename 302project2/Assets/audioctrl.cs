using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class audioctrl : MonoBehaviour {

    public static audioctrl audioctrls;
    public bool soundson;
    public float Volume;
    public GameObject BGmusic;
    public GameObject btnsounds;
    public Sprite soundsison, soundsisoff;
    public gamedata data;
    string datafilepath;
    public bool playsounds;


    // Use this for initialization
    void Start ()
    {
        if (audioctrls == null) { audioctrls = this;}
        datafilepath = "../data/usersetting.txt";
        Loaddata();
        if (playsounds == true)
        {
            
            BGmusic.SetActive(true);
         btnsounds.GetComponent<Image>().sprite = soundsison;
        }
        else
       {
            BGmusic.SetActive(false);
            btnsounds.GetComponent<Image>().sprite = soundsisoff ;
        }
 
    }
    private void Update()
    {
        
    }

    public void Savedata()
    {
        FileStream fs = new FileStream(datafilepath, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(playsounds);
        sw.Close();
        fs.Close();
    }

    public void Loaddata()
    {
        if (File.Exists(datafilepath))
        {
            FileStream fs = new FileStream(datafilepath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
           string  temp=  sr.ReadLine() ;
            if (temp.ToLower() == "true")
            {
                playsounds = true;
            }
            else
            {
                playsounds = false;
            }
            sr.Close();
            fs.Close();
        }
    }

    public void togglesound()
    {
        if(playsounds == true)
        {
            BGmusic.SetActive(false);
            btnsounds.GetComponent<Image>().sprite = soundsisoff;
            playsounds = false;
            Savedata();
        }
        else if(playsounds == false)
        { 
            BGmusic.SetActive(true);
            btnsounds.GetComponent<Image>().sprite = soundsison;
            playsounds = true;
             Savedata();
        }
    }
    
}
