using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;//实例

    public SkillManager skillManager = new SkillManager();//技能管理器实例
    public GoodManager goodManager = new GoodManager();//物品管理器实例

    public Player player;//玩家脚本

    public SkillStoneCreator skillStoneCreator;//技能石创建器
    public SkillParticleCreator skillParticleCreator;//技能特效创建器
    public SkillActionManager skillActionManager;
    public EffectManager effectManager;

    //public CinemachineVirtualCamera virtualCamera;

 //   public Transform spawn;
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        skillManager.InitSkill();
        goodManager.InitGoods();
        skillActionManager.InitSkillCallback();
      ///  InitUI();
      //  InitPlayer();

    }
  //  void InitUI() { 
    //    UIManager ui = Instantiate(Resources.Load("UI/UIManager") as GameObject).GetComponent<UIManager>();
     //   ui.Init();   
 //   }
  //  void InitPlayer()
  //  {
   //     if(virtualCamera == null) { virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>(); }
   //     player =  Instantiate(Resources.Load("Player/Player") as GameObject).GetComponent<Player>();
     //   virtualCamera.Follow = player.transform;
    //    if (spawn != null)
     //   {
     //       player.transform.position = spawn.position;
//        }
//    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LevelUP()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReLoadLevel(int index)
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

}
