using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyManger : MonoBehaviour
{  
    void Start()
    {
        
    }
  
    void Update()
    {
        OpenPanel(UIManger.instance.bagPanel.gameObject, KeyCode.B);
        OpenPanel(UIManger.instance.cheatPanel.gameObject, KeyCode.F7);
    }
   public void OpenPanel(GameObject gameObject,KeyCode keyCode)
    {
        if (Input.GetKeyUp(keyCode))
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
