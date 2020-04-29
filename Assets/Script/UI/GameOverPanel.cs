using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public Button reTry;
    public Button quiet;

    void Start()
    {
        reTry.onClick.AddListener(() =>
        {
            GameManager.instance.ReLoadLevel();
        }); 
        quiet.onClick.AddListener(() =>
        {
            GameManager.instance.LoadLevel(0);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
