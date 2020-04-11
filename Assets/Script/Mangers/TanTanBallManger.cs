using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TanTanBallManger : MonoBehaviour
{
    public GameObject TanTanBall;

    public float time = 5;
    void Start()
    {
        InvokeRepeating("MakeBall", time, time);
    }

    public void MakeBall()
    {
        Instantiate(TanTanBall);
    }
}
