using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodePanel : MonoBehaviour
{
    private InputField codeField;

    private void Start()
    {
        codeField = GetComponent<InputField>();
    }
    void Update()
    {
        if (Input.GetKeyUp(GameManager.instance.keyManager.FindKey(Util.KeyCollection.Enter).keyCode))
        {
            string code = codeField.text;
            switch (code)
            {
                case "CheatPanel":
                    UIManager.instance.cheatPanel.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    break;            
            }
            codeField.text = "";

        }
    }
}
