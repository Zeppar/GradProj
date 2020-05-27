using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class CodePanel : MonoBehaviour
{
    public InputField codeField;
    public Text errorCode;

    private void OnEnable() {
        codeField.text = "";
        GameManager.instance.disableInput = true;
        StartCoroutine(Util.DelayExecute(0.1f, () => {
            codeField.ActivateInputField();
        }));
    }

    private void OnDisable() {
        GameManager.instance.disableInput = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(GameManager.instance.keyManager.FindKey(Util.KeyCollection.Enter).keyCode))
        {
            string code = codeField.text;
            switch (code)
            {
                case "CheatPanel":
                    errorCode.gameObject.SetActive(false);
                    UIManager.instance.cheatPanel.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                default:
                    errorCode.gameObject.SetActive(true);
                    break;
            }
            codeField.text = "";
        }
    }
}
