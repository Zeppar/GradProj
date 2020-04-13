using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour {



    [Header("快捷技能")]
    public Image quickSkill1;
    public Image quickSkill2;
 
  
    [Header("道具背包")]
    public BagPanel bagPanel;
    public GameObject describePanel;

    [Header("面板物体")]
    public GameObject BagPanel_Obj;
    public GameObject Cheat_Obj;
    public HPBarPanel hpBarPabel;

    [Header("死亡界面")]
    public GameObject gameOverPanel;
    public static UIManger instance;

    [Header("提示界面")]
    public GameObject GetItemHelp;
    public GameObject GotItemHelpText;
    private void Awake() {
        instance = this;
        //Init();
    }
 
    public void Init()
    {                  
        hpBarPabel.UpdateHpBar(GameManager.instance.player.HP);
        bagPanel.Init();
    }

    public void LoadLevel(int index) {
        SceneManager.LoadScene(index);
    }
}
