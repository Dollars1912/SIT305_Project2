using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightModeController : MonoBehaviour
{
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



    // TODO hook this currentMode field with the game controller.
    // TODO change it back to private
    public KnightMode currentMode;
       

    
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
