using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// this class is to orgnized the data store in the game.dat file
/// </summary>
[Serializable]
public class gamedata {

    public int coinCount;
    public float exp_total;
    public float exp_now;
    public float exp_percentage;
    public float health_percentage;
    public float health_total;
    public float health_now;
    public int level;
    public bool isFirstBoot;
    public int medicine;

}
