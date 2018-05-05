using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContoller : MonoBehaviour
{
    #region states enum

    public enum ChestState
    {
        Closed,
        Reward,
        Empty
    }

    #endregion

    #region Inspector Fields

    public GameObject ClosedChest;
    public GameObject OpenChest;
    public GameObject Reward;

    public ChestState CurrentMode = ChestState.Closed;

    #endregion

    #region Unity Hook

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
                Reward.SetActive(true);
                break;
            case ChestState.Empty:
                ClosedChest.SetActive(false);
                OpenChest.SetActive(true);
                Reward.SetActive(false);
                break;
        }
    }

    #endregion

    #region Public methods

    public void OnTriggered()
    {
        switch (CurrentMode)
        {
            case ChestState.Closed:
                CurrentMode = ChestState.Reward;
                break;
            case ChestState.Reward:
				CurrentMode = ChestState.Empty;
				break;
            case ChestState.Empty:
                break;
        }
    }

    #endregion
}
