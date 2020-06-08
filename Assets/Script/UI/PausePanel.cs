using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public Button resumeButton;
    public Button setKeyButton;
    public Button BsckMainButton;

    public Button loadBtn;
    public Button saveBtn;
    void Start()
    {
        resumeButton.onClick.AddListener(OnResume);
        setKeyButton.onClick.AddListener(ShowSetKeyPanel);
        loadBtn.onClick.AddListener(() => { GameManager.instance.saveManager.Load(); });
        loadBtn.onClick.AddListener(OnResume);
        saveBtn.onClick.AddListener(() => { GameManager.instance.saveManager.Save(); });
        saveBtn.onClick.AddListener(OnResume);

        //Time.timeScale = 0
    }
    void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    void OnResume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    void ShowSetKeyPanel()
    {
        //Time.timeScale = 1;
        UIManager.instance.keySetPanel.gameObject.SetActive(true);
    }
    

}
