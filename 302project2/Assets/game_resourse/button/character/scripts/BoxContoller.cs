using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContoller : MonoBehaviour
{
    //states enum

    public enum ChestState
    {
        Closed,
        Reward,
        Empty
    }

   // Inspector Fields

    public GameObject ClosedChest;
    public GameObject OpenChest;
    public GameObject Reward;

    public ChestState CurrentMode = ChestState.Closed;

 //Unity Hook

    void FixedUpdate()
    {
        switch (CurrentMode)
        {
            case ChestState.Closed:
                ClosedChest.SetActive(true);
                OpenChest.SetActive(false);
                Reward.SetActive(false);
                break;
            case ChestState.Reward:
                ClosedChest.SetActive(false);
                OpenChest.SetActive(true);
                Destroy(OpenChest, 1);
                Reward.SetActive(true);
                break;
            case ChestState.Empty:
                ClosedChest.SetActive(false);
                OpenChest.SetActive(true);
                Reward.SetActive(false);
                Destroy(OpenChest, 1);
                break;
        }
    }



    public void OnTriggered()
    {
        switch (CurrentMode)
        {
            case ChestState.Closed:
                CurrentMode = ChestState.Reward;
                break;
          
            case ChestState.Empty:
                break;
        }
    }

 
}
