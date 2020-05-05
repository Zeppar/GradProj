using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform spawn;

    public bool isCreateUI = true;
  
    private void Awake()
    {
        InitGameManager();
        InitPlayer();
        if (isCreateUI)
        {
            InitUI();
        }
        Destroy(gameObject);
    }
    public GameManager InitGameManager()
    {
       return Instantiate(Resources.Load("Manager/GameManager") as GameObject).GetComponent<GameManager>();
    }
    public void InitPlayer()
    {
        if (virtualCamera == null) { virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>(); }
         GameManager.instance.player = Instantiate(Resources.Load("Player/Player") as GameObject).GetComponent<Player>();
        virtualCamera.Follow = GameManager.instance.player.transform;
        GameManager.instance.effectManager.cinemaInpulse = virtualCamera.GetComponent<CinemachineCollisionImpulseSource>();
        if (spawn != null)
        {
            GameManager.instance.player.transform.position = spawn.position;
        }        
    }
    public void InitUI()
    {
        UIManager ui = Instantiate(Resources.Load("UI/UIManager") as GameObject).GetComponent<UIManager>();
        ui.Init();
    }
 
}
