using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static GameManager instance;//实例

    public SkillManager skillManager = new SkillManager();//技能管理器实例
    public GoodManager goodManager = new GoodManager();//物品管理器实例
    public EnemyManager enemyManager = new EnemyManager();//怪物管理器实例
    public EnergyManager energyManager = new EnergyManager(0, 100.0f);
    public LevelManager levelManager = new LevelManager();
    public AutoSaveManager autoSaveManager = new AutoSaveManager();
    public KeyManager keyManager = new KeyManager();
    public SaveManager saveManager = new SaveManager();

    [HideInInspector]
    public Player player;//玩家脚本

    public SkillStoneCreator skillStoneCreator;//技能石创建器
    public SkillParticleCreator skillParticleCreator;//技能特效创建器
    public SkillActionManager skillActionManager;
    public EffectManager effectManager;

    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    public bool gameStart;
    public bool showEndPoint;
    public bool disableInput;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

            skillManager.InitSkill();
            goodManager.InitGoods();
            enemyManager.InitEnemy();
            keyManager.Init();
            skillActionManager.InitSkillCallback();
            levelManager.Init();
            InitUI();

        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    void InitUI() {
        StartCoroutine(Util.DelayExecute(() => {
            return player != null;
        }, () => {
            UIManager ui = Instantiate(Resources.Load("UI/UIManager") as GameObject).GetComponent<UIManager>();
            ui.Init();
        }));
    }

    public void LevelUp() {
        Util.LevelOp.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        levelManager.EnterNextLevel();
    }
    
    public void InitPlayer(Vector3 pos) {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        player = Instantiate(Resources.Load("Player/Player") as GameObject).GetComponent<Player>();
        player.transform.position = pos;
        virtualCamera.Follow = player.transform;
    }

}
