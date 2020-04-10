using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanTanBallManger : MonoBehaviour
{
    public GameObject TanTanBall;
    void Start()
    {
        InvokeRepeating("MakeBall", 5, 5);
    }

    public void MakeBall()
    {
        Instantiate(TanTanBall);
    }
}
