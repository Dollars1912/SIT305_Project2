using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menucontrol : MonoBehaviour {
    
    void Start()
    {
    }

public void Loadscene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

}
