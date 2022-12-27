using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.U2D.Animation;

public class TaskManager : MonoBehaviour
{
    static TaskManager instance = null;
    public static TaskManager i
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TaskManager>();
            }
            return instance;
        }
    }

    [SerializeField] List<Task> taskBoard = new List<Task>();
    [SerializeField] AllTaskInfo ati = null;
    [SerializeField] GameObject taskTemplate;
    [SerializeField] List<Transform> taskPlaces;
    [SerializeField] TaskInfo displayingNow = null;
    Task displayingTask = null;
    Transform taskDescription;

    private void Awake()
    {
        taskDescription = transform.Find("Task Description");
    }

    private void Start()
    {
        addTask(ati.info[0]);
        //SelectTask(taskBoard[0]);
        CheckTasksCompleteState();
        taskBoard[0].SelectInScript();
        //addTask(ati.info[1]);
        //addTask(ati.info[2]);
    }

    public void addTask(TaskInfo ti)
    {
        if (ti.completion != 0) return;
        if (!canAddTask()) return;

        GameObject newTask = Instantiate(taskTemplate);
        //("taskBoard.count: "+ taskBoard.Count);
        //print("targetPosition: " + taskPlaces[taskBoard.Count].transform.position);
        newTask.transform.position = taskPlaces[taskBoard.Count].transform.position;
        taskBoard.Add(newTask.GetComponentInChildren<Task>());
        newTask.GetComponentInChildren<Task>().SetUp(ti);
        ti.completion = 1;
    }

    public void CheckTasksCompleteState()
    {
        foreach(Task t in taskBoard)
        {
            bool completion = true;
            if (t.myTi.request == null || t.myTi.request.Count == 0) 
            {
                if (ProgressControl.i.CheckCompletion(t.myTi))
                {
                    t.myTi.completion = 2;
                }
                else
                {
                    t.myTi.completion = 1;
                }
            }
            else
            {
                 foreach(KeyValuePair<ItemScriptableObject, int> kvp in t.myTi.request)
                {
                    if(Inventory.i.AmountOf(kvp.Key) < kvp.Value)
                    {
                        completion = false;
                    }
                }
                if (completion)
                {
                    t.myTi.completion = 2;
                }
                else
                {
                    t.myTi.completion = 1;
                }
            }
        }
        RefreshCompletion();
    }

    void RefreshCompletion()
    {
        if (displayingNow == null) return;
        if (displayingNow.completion == 2)
        {
            transform.Find("Task Complete Button").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("Task Complete Button").gameObject.SetActive(false);
        }
    }

    public void ClickComplete()
    {
        taskBoard.Remove(displayingTask);
        Destroy(displayingTask.transform.parent.gameObject);
        displayingTask = null;

        transform.Find("Task Figure").gameObject.SetActive(false);
        transform.Find("Task Description").gameObject.SetActive(false);
        transform.Find("Task UI Background").gameObject.SetActive(false);

        transform.Find("Task Complete Button").gameObject.SetActive(false);

        for (int i =0; i<taskBoard.Count; i++)
        {
           taskBoard[i].transform.parent.transform.position = taskPlaces[i].transform.position;
        }

        foreach(TaskInfo iso in displayingNow.unlocks)
        {
            addTask(iso);
        }

        displayingNow.completion = 3;
        displayingNow = null;
    }

     void DisplayTask(TaskInfo ti)
    {
        if (displayingNow ==null)
        {            
            transform.Find("Task Figure").gameObject.SetActive(true);
            transform.Find("Task Description").gameObject.SetActive(true);
            transform.Find("Task UI Background").gameObject.SetActive(true);
        }

        displayingNow = ti;
        taskDescription.GetComponent<TextMeshPro>().text = displayingNow.descriptionText;

        RefreshCompletion();

        if (ti.category == TaskInfo.Category.Jessie)
        {
            transform.Find("Task Figure").GetComponent<SpriteResolver>().SetCategoryAndLabel("Standing Figures", "Jessie");
        }
        else if (ti.category == TaskInfo.Category.Adam)
        {
            transform.Find("Task Figure").GetComponent<SpriteResolver>().SetCategoryAndLabel("Standing Figures", "Adam");
        }
        else if (ti.category == TaskInfo.Category.Charles)
        {
            transform.Find("Task Figure").GetComponent<SpriteResolver>().SetCategoryAndLabel("Standing Figures", "Charles");
        }
        else if (ti.category == TaskInfo.Category.Julie)
        {
            transform.Find("Task Figure").GetComponent<SpriteResolver>().SetCategoryAndLabel("Standing Figures", "Julie");
        }
        else if (ti.category == TaskInfo.Category.Smith)
        {
            transform.Find("Task Figure").GetComponent<SpriteResolver>().SetCategoryAndLabel("Standing Figures", "Smith");
        }
    }

    public void SelectTask(Task t)
    {
        foreach(Task i in taskBoard)
        {
            if (t != i) 
            {
                //print("deselect: " + i.transform.parent.gameObject.name);
                i.Deselect(); 
            }
        }
        DisplayTask(t.myTi);
        displayingTask = t;
    }

    public bool canAddTask()
    {
        if (taskBoard.Count > 5) return false;
        return true;
    }



}
