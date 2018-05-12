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
                OpenChest.SetActive(false);
                Reward1.SetActive(false);
                Reward2.SetActive(false);
                break;
            case ChestState.Reward1:
                ClosedChest.SetActive(false);
                OpenChest.SetActive(true);
                Destroy(OpenChest, 1);
                Reward1.SetActive(true);
                Reward2.SetActive(false);
                break;
            case ChestState.Reward2:
                ClosedChest.SetActive(false);
                OpenChest.SetActive(true);
                Destroy(OpenChest, 1);
                Reward1.SetActive(false);
                Reward2.SetActive(true);
                break;
            case ChestState.Empty:
                ClosedChest.SetActive(false);
                OpenChest.SetActive(true);
                Reward1.SetActive(false);
                Reward2.SetActive(false);
                Destroy(OpenChest, 1);
                break;
        }
    }



    public void OnTriggered()
    {
        switch (CurrentMode)
        {
            case ChestState.Closed:
                CurrentMode = ChestState.Reward1;
                break;
          
            case ChestState.Empty:
                break;
        }
       /* if (ChestState.Closed = true)
        {
            CurrentMode = 
        }*/
    }

 
}
