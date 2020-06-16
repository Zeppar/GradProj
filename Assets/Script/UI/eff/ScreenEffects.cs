using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    public Loadeff loadeff;

    private void Start()
    {
        loadeff.gameObject.SetActive(true);
    }
}
