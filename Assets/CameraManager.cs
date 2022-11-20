using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera exploreCVC, panelCVC;
    int CamNow = 1;

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
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CamNow == 1)
            {
                switchCameraTo(2);
            }else if(CamNow == 2)
            {
                switchCameraTo(1);
            }
        }
    }

    /*public Camera getPanelCamera()
    {
        return panelRC;
    }*/

    /// <summary>
    /// Switch camera accroding to int.
    /// </summary>
    /// <param name="which">1: explore 2: panel</param>
    public void switchCameraTo(int which)
    {
        if(which == 1)
        {
            exploreCVC.Priority = 11;
            panelCVC.Priority = 10;
        }
        else if (which == 2)
        {
            exploreCVC.Priority = 10;
            panelCVC.Priority = 11;
        }
        else
        {
            print("No such camera to switch to");
        }
        CamNow = which;
    }
}
