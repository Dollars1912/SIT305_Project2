using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContoller : MonoBehaviour
{
    //states enum

    public enum ChestState
    {
        Closed,
        Reward1,
        Reward2,
        Empty
    }

   // Inspector Fields

    public GameObject ClosedChest;
    public GameObject OpenChest;
    public GameObject Reward1;
    public GameObject Reward2;

    public ChestState CurrentMode = ChestState.Closed;

 //Unity Hook

    void FixedUpdate()
    {
        switch (CurrentMode)
        {
            case ChestState.Closed:
                ClosedChest.SetActive(true);                
                Reward1.SetActive(false);
                Reward2.SetActive(false);
                break;
            case ChestState.Reward1:
                ClosedChest.SetActive(false);
                Destroy(OpenChest, 1);
                Reward1.SetActive(true);
                Reward2.SetActive(false);
                break;
            case ChestState.Reward2:
                ClosedChest.SetActive(false);
                Destroy(OpenChest, 1);
                Reward1.SetActive(false);
                Reward2.SetActive(true);
                break;
            case ChestState.Empty:
                ClosedChest.SetActive(false);
                Destroy(OpenChest, 1);
                Reward1.SetActive(false);
                Reward2.SetActive(false);
                break;
        }
    }

   

    public void OnTriggered()
    {
        switch (CurrentMode)
        {
            case ChestState.Closed:
                if (Knight.knights.Ispowerpup == false)
                    CurrentMode = ChestState.Reward1;
                else //(Knight.knights.Ispowerpup == true)
                    CurrentMode = ChestState.Reward2;
                break;
          
            case ChestState.Empty:
                break;
        }

    }

 
}
