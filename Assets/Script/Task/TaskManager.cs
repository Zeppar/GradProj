using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Dictionary<int,TaskInfo> taskDic = new Dictionary<int, TaskInfo>();
    public bool isdirty = false;
    public void Init()
    {
        TaskInfo[] taskInfos = Resources.LoadAll<TaskInfo>("Task");
        for (int i = 0; i < taskInfos.Length; i++)
        {
            taskDic.Add(i, taskInfos[i]);
        }
    }
    public void AcceptTask(int id)
    {
        print("A");
        if (taskDic.ContainsKey(id))
        {
            taskDic[id].taskStates = TaskInfo.TaskStates.doing;
            isdirty = true;
        }
        else
        {
            Debug.LogError("Unknow Task");
        }
    }

    public List<TaskInfo> GetAcceptedTask()
    {

        List<TaskInfo> list = new List<TaskInfo>();
        foreach (var item in taskDic)
        {
            if(item.Value.taskStates == TaskInfo.TaskStates.doing)
            {
                list.Add(item.Value);
            }
        }
        if(list.Count !=0) {
            foreach (var item in list)
            {
                print(item.taskStates);
            }
            print(1);
            return list;
        }
        else
        {
            print(2);
            return null;
        }
    }
}
