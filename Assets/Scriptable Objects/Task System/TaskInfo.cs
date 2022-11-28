using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Task Info", menuName = "ScriptableObjects/TaskInfo")]
public class TaskInfo : SerializedScriptableObject
{
    public enum Category {Regular, Mom, Dad}
    public Category category;
    public string taskName;
    public string personNam;
    public Sprite personPic;
    public string descriptionText;
    public string tradeText;
    public Dictionary<ItemScriptableObject, int> request;
    public Dictionary<ItemScriptableObject, int> reward;
    /// <summary>
    /// 0: not assigned 1: assigned 2: completed
    /// </summary>
    public int completion;
    public TaskInfo unlocks;
}
