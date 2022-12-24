using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera worldCMVC, craftingCMVC, taskCMVC, shopCMVC;
    [SerializeField] int CamNow = 2;
    [SerializeField] GameObject panelBoarder;

    static CameraManager instance;
    public static CameraManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CameraManager>();
            }
            return instance;
        }
    }
    

    /*public Camera getPanelCamera()
    {
        return panelRC;
    }*/

    /// <summary>
    /// Switch camera accroding to int.
    /// </summary>
    /// <param name="which">1: world 2: craft 3: task</param>
    public void switchCameraTo(int which)
    {
        if(which == 1)
        {
            panelBoarder.SetActive(false);
        }
        else
        {
            panelBoarder.SetActive(true);
        }

        if(which == 1)
        {
            Camera.main.orthographic = false;
            worldCMVC.Priority = 11;
            craftingCMVC.Priority = 10;
            taskCMVC.Priority = 10;
            shopCMVC.Priority = 10;

        }
        else if (which == 2)
        {
            Camera.main.orthographic = true;
            worldCMVC.Priority = 10;
            craftingCMVC.Priority = 11;
            taskCMVC.Priority = 10;
            shopCMVC.Priority = 10;

        }
        else if(which == 3)
        {
            Camera.main.orthographic = true;
            worldCMVC.Priority = 10;
            craftingCMVC.Priority = 10;
            taskCMVC.Priority = 11;
            shopCMVC.Priority = 10;

        }
        else if (which == 4)
        {
            Camera.main.orthographic = true;
            worldCMVC.Priority = 10;
            craftingCMVC.Priority = 10;
            taskCMVC.Priority = 10;
            shopCMVC.Priority = 11;
        }
        else
        {
            print("No such camera to switch to");
        }
        CamNow = which;
    }
}
