using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyCount : MonoBehaviour
{
    public Button keySetButton;
    public Text keySetMotd;
    public string keyId;

    private void Start()
    {
        keySetButton.onClick.AddListener(()=>
        {
            keySetButton.GetComponentInChildren<Text>().text = "...";
            StartCoroutine("SetKey");
        }
            );
    }
    public void SetCount(KeyCode _keyCode, string motd = null,string id = null)
    {
        keySetButton.GetComponentInChildren<Text>().text = _keyCode.ToString();

        if(motd == null)
        {
            return;
        }
        
        keySetMotd.text = motd;
        if(id != null)
        {
            keyId = id;
        }
    }
    public KeyCode currKeycode()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {              
                    return keyCode;
                }

            }      
        }
        return 0;
     
    }
    public IEnumerator SetKey()
    {
        yield return new WaitUntil(() => {

        if (currKeycode() != 0) {
                KeyCode curkey = currKeycode();
                SetCount(curkey);
                SetKeyData(keyId,curkey);
                return true;               
            }      
         else
          {                
               return false;
            }            
        });
      
    }
    public void SetKeyData(string id,KeyCode key)
    {
        foreach (var item in GameManager.instance.keyManager.keyInfos)
        {
            if (item.Value.id == id)
            {
                item.Value.key = key;
            }
        }
    }
}
