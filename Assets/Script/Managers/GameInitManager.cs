using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform spawn;
    public GameManager gameManager;


    public bool isCreateUI = true;
  
    private void Awake()
    {
        InitGameManager();
        InitPlayer(spawn.position);
        Destroy(gameObject);
    }
    public void InitGameManager()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameManager);
        }
        else
        {
            gameManager.InitInstance();
        }
    }
    public void InitPlayer(Vector3 pos)
    {
        if (virtualCamera == null)
        {
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }
        GameManager.instance.player = Instantiate(Resources.Load("Player/Player") as GameObject).GetComponent<Player>();
        GameManager.instance.player.transform.position = pos;
        virtualCamera.Follow = GameManager.instance.player.transform;
    }


}
