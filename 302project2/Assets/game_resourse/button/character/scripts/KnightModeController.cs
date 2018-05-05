using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightModeController : MonoBehaviour
{
	#region State enum
	public enum KnightMode
	{
		Short,
		Medium,
		Long
	}
    #endregion

    #region Inspector Fields

    public SpriteRenderer KnightSpriteRender;

	#endregion

    #region Private Fields

    // TODO hook this currentMode field with the game controller.
    // TODO change it back to private
    public KnightMode currentMode;

    #endregion

    #region Unity Hook

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

    #endregion
}
