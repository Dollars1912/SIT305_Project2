using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UIctrl {
    /// <summary>
    /// the list for the ui menu object
    /// </summary>

    [Header("Text")]
    public Text txtCoinCount;
    public Text exp;
    public Text txtTimer;
    public Text level;
    [Header("Images/Sprites")]
    public Image img_health;
    public Image img_exp;


}
