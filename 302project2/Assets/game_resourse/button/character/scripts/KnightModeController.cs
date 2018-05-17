using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightModeController : MonoBehaviour
{/// <summary>
/// this class will changing the appearence of the player when they power up and super powerup
/// </summary>
    public static KnightModeController knightmodectrl;
    private void Awake()
    {
        if (knightmodectrl == null)
            knightmodectrl = this;


    }
    public enum KnightMode
	{
		Short,
		Medium,
		Long
	}
   

    public SpriteRenderer KnightSpriteRender;
    public KnightMode currentMode;
       

    //determine the current mode of the knight
    void setmode()
    {
        if(Knight.knights.Ispowerpup == true&& Knight.knights.isbetterrpowerup == false)
        {
            currentMode = KnightMode.Medium;

        }
        if (Knight.knights.Ispowerpup == true && Knight.knights.isbetterrpowerup == true)
        {
            currentMode = KnightMode.Long;

        }
        if (Knight.knights.Ispowerpup == false && Knight.knights.isbetterrpowerup == false)
        {
            currentMode = KnightMode.Short;

        }
    }
    /// <summary>
    /// for different mode scwitch the appearence of the player to make the game attactive
    /// </summary>
    void FixedUpdate()
    {
        switch (currentMode)
        {
            case KnightMode.Short:
                KnightSpriteRender.color = Color.white;
                break;
            case KnightMode.Medium:
                KnightSpriteRender.color = Color.yellow;
				break;
            case KnightMode.Long:
				KnightSpriteRender.color = Color.red;
				break;
        }
    }

}
