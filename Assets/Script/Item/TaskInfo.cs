using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskInfo", menuName = "ChaseTheLight/New Task  Info")]
public class TaskInfo : ScriptableObject
{
    public string taskName;
    public string taskDes;
    public TaskStates taskStates = TaskStates.unaccept;
    public enum TaskStates
    {
        unaccept,
        doing,
        finish
    }

}
