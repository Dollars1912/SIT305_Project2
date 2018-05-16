using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UIctrl {


    [Header("Text")]
    public Text txtCoinCount;
    public Text exp;
    public Text txtTimer;
    public Text level;
    public Text health_total;
    public Text health_now;
    [Header("Images/Sprites")]
    public Image img_health;
    public Image img_exp;

    [Header("Pause_panel")]
    public GameObject panelPause;
    public GameObject mobileui;

}
