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
        OpenPanel(UIManager.instance.bagPanel.gameObject, KeyCode.B);
        OpenPanel(UIManager.instance.cheatPanel.gameObject, KeyCode.F7);
    }

    public void OpenPanel(GameObject gameObject, KeyCode keyCode) {
        if (Input.GetKeyUp(keyCode)) {
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        }
    }
}
