﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyManager : MonoBehaviour
{  
    void Start()
    {
        
    }
  
    void Update()
    {
        OpenPanel(UIManager.instance.bagPanel.gameObject, KeyCode.B);
        OpenPanel(UIManager.instance.cheatPanel.gameObject, KeyCode.F7);
        OpenPanel(UIManager.instance.pausePanel.gameObject, KeyCode.Escape);
        OpenPanel(UIManager.instance.codePanel.gameObject, GameManager.instance.keyManager.FindKey(Util.KeyCollection.Cheat).keyCode);
        OpenPanel(UIManager.instance.taskPanel.gameObject, KeyCode.T);
        
    }
   
    public void OpenPanel(GameObject gameObject, KeyCode keyCode, bool canInvoke = true) {
        if (!canInvoke)
            return;
        if (Input.GetKeyDown(keyCode))
            gameObject.SetActive(!gameObject.activeSelf);
    }
}
