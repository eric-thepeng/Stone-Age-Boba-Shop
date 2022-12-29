using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressControl : MonoBehaviour
{
    List<TaskInfo> completedPCTasks = new List<TaskInfo>();
    static ProgressControl instance;

    //task 1 related
    [SerializeField] TaskInfo task1;
    bool task1_freed = false, task1_viewedShop = false, task1_viewedCraft = false;

    public static ProgressControl i
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ProgressControl>();
            }
            return instance;
        }
    }

    public bool CheckCompletion(TaskInfo ti)
    {
        if (completedPCTasks.Contains(ti)) return true;
        return false;
    }

    public void Free_FirstTask(int panelsTraveled)
    {
        if (task1_freed) return;

        if(panelsTraveled == 2)
        {
            task1_viewedCraft = true;
        }
        else if (panelsTraveled == 4)
        {
            task1_viewedShop = true;
        }

        if(task1_viewedCraft && task1_viewedShop)
        {
            task1_freed = true;
            completedPCTasks.Add(task1);
        }
        
    }

    private void Awake()
    {
        //completedPCTasks.Add(task1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
