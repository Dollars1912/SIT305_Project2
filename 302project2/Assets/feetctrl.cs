using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetctrl : MonoBehaviour {

    public Transform dustSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            SFXctrl.sfxcontrol.ShowlandedSparkle(dustSFX.position);
        }
    }
}
