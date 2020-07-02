using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : MonoBehaviour
{

    public TaskItem taskItem;
    public Transform parent;

    List<TaskItem> itemBeAdd = new List<TaskItem>();
    public void AddItem(TaskInfo info)
    {
        TaskItem ToAddItem = Instantiate(taskItem);
        ToAddItem.transform.SetParent(parent);
        ToAddItem.Init(info.name);
        itemBeAdd.Add(ToAddItem);
    }
    private void Update()
    {
        if (GameManager.instance.taskManager.isdirty)
        {

            GameManager.instance.taskManager.isdirty = false;
            UpdataItem();
        }
    }

    public void UpdataItem()
    {
        foreach (var item in itemBeAdd)
        {
            Destroy(item);
        }
        itemBeAdd.Clear();
        List<TaskInfo> acceptInfo = GameManager.instance.taskManager.GetAcceptedTask();
        if(acceptInfo == null)
        {
            return;
        }
        foreach (var item in acceptInfo)
        {
            if(item.taskStates == TaskInfo.TaskStates.doing) { 
                    AddItem(item);
                }
        }
    }
}
