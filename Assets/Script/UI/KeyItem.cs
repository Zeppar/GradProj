﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyItem : MonoBehaviour {
    public Button keySetButton;
    public Text keyDesText;
    private string keyId;
    private Text btnText;
    private KeyInfo info;

    private void Start() {
        keySetButton.onClick.AddListener(() => {
            btnText.text = "...";
            StartCoroutine("SetKey");
        });
    }

    public void SetContent(KeyInfo info) {
        this.info = info;
        if(btnText == null)
            btnText = keySetButton.GetComponentInChildren<Text>();
        btnText.text = info.keyCode.ToString();
        keyDesText.text = info.des;
        keyId = info.id;
    }

    public KeyCode GetInputKey() {
        if (Input.anyKeyDown) {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(keyCode)) {
                    return keyCode;
                }
            }
        }
        return 0;
    }

    public IEnumerator SetKey() {
        yield return new WaitUntil(() => {
            if (GetInputKey() != 0) {
                KeyCode curkey = GetInputKey();
                GameManager.instance.keyManager.SetKeyData(keyId, curkey, info.keyCode, () => {
                    SetContent(info);
                });
                return true;
            } else {
                return false;
            }
        });
    }
    
}