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
            if(instance == null)
            {
                instance = FindObjectOfType<TaskManager>();
            }
            return instance;
        }
    }

    List<Task> taskBoard = new List<Task>();
    [SerializeField]AllTaskInfo ati=null;
    [SerializeField] GameObject taskTemplate;
    TaskInfo displayingNow = null;
    Transform taskDescription;

    private void Awake()
    {
        taskDescription = transform.Find("Task Description");
    }

    private void Start()
    {
        addTask(ati.info[0]);
    }

    public void addTask(TaskInfo ti)
    {
        if (ti.completion != 0) return;
        if (!canAddTask()) return;

        Task newTask = Instantiate(taskTemplate).GetComponent<Task>();
        taskBoard.Add(newTask);
        newTask.SetUp(ti);
    }

    public void DisplayTask(TaskInfo ti)
    {
        displayingNow = ti;
        taskDescription.GetComponent<TextMeshPro>().text = ti.descriptionText;

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
