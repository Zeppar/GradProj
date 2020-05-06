using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyManager : MonoBehaviour
{  
    void Start()
    {
        
    }
  
    void Update()
    {
        OpenPanel(UIManager.instance.bagPanel.gameObject, KeyCode.B, GameManager.instance.levelManager.currentInfo.bagEnable);
        OpenPanel(UIManager.instance.cheatPanel.gameObject, KeyCode.F7);
    }

    public void OpenPanel(GameObject gameObject, KeyCode keyCode, bool canInvoke = true) {
        if (!canInvoke)
            return;
        if (Input.GetKeyUp(keyCode))
            gameObject.SetActive(!gameObject.activeSelf);
    }
}
