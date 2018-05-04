using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontrol : MonoBehaviour {
    /// <summary>
    /// load scene when user click start game in menu
    /// </summary>
    /// <param name="scenename"></param>
public void Loadscene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
