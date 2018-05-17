using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menucontrol : MonoBehaviour {
    /// <summary>
    /// the controller of the menu page
    /// </summary>
    void Start()
    {
    }

public void Loadscene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void quitgame()
    {
        Application.Quit();
    }

}
