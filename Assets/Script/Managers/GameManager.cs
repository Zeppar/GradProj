﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    // Start is called before the first frame update
    public static GameManager instance;//实例

    public SkillManager skillManager = new SkillManager();//技能管理器实例
    public GoodManager goodManager = new GoodManager();//物品管理器实例
    public EnemyManager enemyManager = new EnemyManager();//怪物管理器实例
    public EnergyManager energyManager = new EnergyManager(0, 100.0f);
    public LevelManager levelManager = new LevelManager();
    public AutoSaveManager autoSaveManager = new AutoSaveManager();
    public KeyManager keyManager = new KeyManager();

    public Player player;//玩家脚本

    public SkillStoneCreator skillStoneCreator;//技能石创建器
    public SkillParticleCreator skillParticleCreator;//技能特效创建器
    public SkillActionManager skillActionManager;
    public EffectManager effectManager;
    


    public CinemachineVirtualCamera virtualCamera;

    public Transform spawn;

    public GameObject MiniMap;

    public bool gameStart;
    public bool showEndPoint;

    void Awake() {
        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        } else if (instance != this) {
            Destroy(gameObject);
        }

        skillManager.InitSkill();
        goodManager.InitGoods();
        enemyManager.InitEnemy();
        keyManager.Init();
        skillActionManager.InitSkillCallback();
        levelManager.Init();
        InitPlayer(spawn.position);
        InitUI();
        InitMiniMap();
    }

    void InitMiniMap()
    {
        if(MiniMap != null)
        {
            MiniMap.SetActive(true);
        }
    }

    void InitUI() {
        UIManager ui = Instantiate(Resources.Load("UI/UIManager") as GameObject).GetComponent<UIManager>();
        ui.Init();
    }

    public void InitPlayer(Vector3 pos) {
        if (virtualCamera == null) {
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }
        player = Instantiate(Resources.Load("Player/Player") as GameObject).GetComponent<Player>();
        player.transform.position = pos;
        virtualCamera.Follow = player.transform;
    }

    public void LoadLevel(int index) {
        SceneManager.LoadScene(index);
    }

    public void LevelUp() {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        levelManager.EnterNextLevel();
    }
    public void ReLoadLevel() {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

}
