using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class router : MonoBehaviour {

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
