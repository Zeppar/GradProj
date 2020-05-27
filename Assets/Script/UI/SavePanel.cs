using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    public Button loadBtn;
    public Button saveBtn;
    void Start()
    {
        loadBtn.onClick.AddListener(() => { GameManager.instance.saveManager.LoadGood(); });
        saveBtn.onClick.AddListener(() => { GameManager.instance.saveManager.SaveGood(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
