using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class router : MonoBehaviour {
    /// <summary>
    /// a router that make the panel easier to adapt the method in gamectrl class
    /// </summary>
	public void showpausepanel()
    {
        gamectrl.gamecontrl.showPause();
    }
    public void hidepausepanel()
    {
        gamectrl.gamecontrl.hidePause();
    }
    public void Togglesounds()
    {
        audioctrl.audioctrls.togglesound();
    }
}
