using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    static MasterManager instance=null;
    static MasterManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MasterManager>();
            }
            return instance;
        }
    }

    public enum PlayerState {World, Crafting, Task, Shop}
    [SerializeField] public PlayerState playerState;

    private void Update()
    {
        if(playerState == PlayerState.World) //Player at exploration
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GoHome();
            }
        }
        else //Player at Home
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LeaveHome();
            }

            if(playerState == PlayerState.Crafting && Input.GetKeyDown(KeyCode.A)) //Crafting -> Shop
            {
                GoToShop();
            }

            if (playerState == PlayerState.Crafting && Input.GetKeyDown(KeyCode.D)) //Crafting -> Task
            {
                GoToTask();
            }

            if (playerState == PlayerState.Task && Input.GetKeyDown(KeyCode.A)) //Task -> Crafting
            {
                GoToCrafting();
            }

            if (playerState == PlayerState.Shop && Input.GetKeyDown(KeyCode.D)) //Shop -> Crafting
            {
                GoToCrafting();
            }
        }
    }


    public void GoHome()
    {
        CameraManager.i.switchCameraTo(2);
        playerState = PlayerState.Crafting;
    }

    public void LeaveHome()
    {
        CameraManager.i.switchCameraTo(1);
        playerState = PlayerState.World;
    }

    void GoToCrafting()
    {
        CameraManager.i.switchCameraTo(2);
        playerState = PlayerState.Crafting;
    }

    void GoToTask()
    {
        CameraManager.i.switchCameraTo(3);
        playerState = PlayerState.Task;
    }

    void GoToShop()
    {
        CameraManager.i.switchCameraTo(4);
        playerState = PlayerState.Shop;
    }


}
