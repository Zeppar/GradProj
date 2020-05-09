﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelBeginShowPoint : MonoBehaviour
{
    public Transform point;
  //  public Vector3 startTranform;
    public float speed;
    public bool wasgo = false;
    public bool isShowPoint;

    public string text;

    // Update is called once per frame
    private void Start()
    {
     // startTranform = transform.position;
    }
    void Update()
    {
        if (!isShowPoint)
        {
            UIManager.instance.helpPanel.topTip.ShowTopTip(text);
            gameObject.GetComponent<LevelBeginShowPoint>().enabled = false;
            gameObject.GetComponent<CinemachineBrain>().enabled = true;
        }
        if (!wasgo)
        {
            float step = speed * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, point.position, step);

            if (transform.position == point.position)
            {
                GameManager.instance.StartCoroutine(Util.DelayExecute(1f, () => {
                    wasgo = true;
                }));
                
            }
        }
        if (wasgo)
        {
            float step = speed * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, GameManager.instance.player.transform.position, step);
            if (transform.position == GameManager.instance.player.transform.position)
            {
               // Screeneff.instance.setSceneToClear();
                gameObject.GetComponent<LevelBeginShowPoint>().enabled = false;
                gameObject.GetComponent<CinemachineBrain>().enabled = true;
                
              //  Screeneff.instance.setSceneToClean();
                GameManager.instance.StartCoroutine(Util.DelayExecute(2f, () => {
                 //   Screeneff.instance.setSceneToClear();
                    UIManager.instance.helpPanel.topTip.ShowTopTip(text);
                    GameManager.instance.gameStart = true;
                }));
                
            }
        }
     
    }
}
