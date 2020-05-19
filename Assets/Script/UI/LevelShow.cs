using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelShow : MonoBehaviour
{
    public int showLevel = 0;
    public Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        //image.enabled = false;
    }
    public void AddLevel()
    {
        showLevel++;
        if(showLevel > 5)
        {
            Debug.LogError("ShowLevle is So High");
        }
        if(image == null)
        {
            image = GetComponent<Image>();
        }
        switch (showLevel)
        {
            case 1:
                //image.enabled = true;
                image.color = Color.white;
                break;
            case 2:
                image.color = Color.green;
                break;
            case 3:
                image.color = Color.blue;
                break;
            case 4:
                image.color = new Color(128,0,128);
                break;
            default:
                break;
        }
    }
}
