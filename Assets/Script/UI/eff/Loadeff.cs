using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Loadeff : MonoBehaviour
{
    public RawImage rawImage;
    public float fadeSpeed;
    public bool toClean =false;
    public bool toBlack =false;
    
    public void ToClean()
    {
        rawImage.color = Color.black;
        toBlack = false;
        toClean = true;
        Debug.Log("ToClean");
       
    }  
    public void ToBlack()
    {
        rawImage.color = Color.clear;
        toClean = false;
        toBlack = true;
    }
    private void Update()
    {
        if (toClean)
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);

            if (rawImage.color.a < 0.05f)
            {
                rawImage.color = Color.clear;
                rawImage.enabled = false;
                toClean = false;
            }
        }
        if (toBlack)
        {
            rawImage.enabled = true;
            rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
            if (rawImage.color.a > 0.95f)
            {
                rawImage.color = Color.black;
                toBlack = false;
            }
        }
    }

}
