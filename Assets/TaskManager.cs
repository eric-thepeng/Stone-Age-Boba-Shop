using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    TaskInfo displayingNow = null;
    Transform taskDescription;

    private void Awake()
    {
        taskDescription = transform.Find("Task Description");
    }

    private void Start()
    {
        addTask(ati.info[0]);
        addTask(ati.info[1]);
        addTask(ati.info[2]);
    }

    public void addTask(TaskInfo ti)
    {
        if (ti.completion != 0) return;
        if (!canAddTask()) return;

        GameObject newTask = Instantiate(taskTemplate);
        print("taskBoard.count: "+ taskBoard.Count);
        print("targetPosition: " + taskPlaces[taskBoard.Count].transform.position);
        newTask.transform.position = taskPlaces[taskBoard.Count].transform.position;
        taskBoard.Add(newTask.GetComponentInChildren<Task>());
        newTask.GetComponentInChildren<Task>().SetUp(ti);
    }

     void DisplayTask(TaskInfo ti)
    {
        displayingNow = ti;
        taskDescription.GetComponent<TextMeshPro>().text = ti.descriptionText;
    }

    public void SelectTask(Task t)
    {
        foreach(Task i in taskBoard)
        {
            if (t != i) 
            {
                print("deselect: " + i.transform.parent.gameObject.name);
                i.Deselect(); 
            }
        }
        DisplayTask(t.myTi);
    }

    public void CompleteTask()
    {

    }

    public bool canAddTask()
    {
        if (taskBoard.Count > 5) return false;
        return true;
    }

}
