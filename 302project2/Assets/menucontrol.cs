using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontrol : MonoBehaviour {

public void Loadscene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void resetscene()
    {
        gamectrl.gamecontrl.ResetData();
    }
}
