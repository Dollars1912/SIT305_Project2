using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
/// <summary>
/// this class is forr audio ctrl, all the audio features will maintains here.
/// </summary>
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
    /// <summary>
    /// save data and load data will saving the data value called "playsounds"in the external file "usersetting.dat"
    /// these method will been called wherever the user change the settings of the game.(exp: user turn on and turn off the sounds orr change the volume of the sounds)   exp savedata(),loaddata()
    /// for save data, it will retured a writeline wrritten the data in the file line by line. when loading data, analyize the data store in the file and return to the corrrect data type and value declared above.
    /// </summary>
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
    /// <summary>
    /// this mthod is to control the sounds on/off button. it will been called when sounds button click, and will save the user setting in the file.
    /// </summary>
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
