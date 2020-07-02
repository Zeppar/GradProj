using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public Text text;
    public void Init(string _text)
    {
        text.text = _text;
    }
}
