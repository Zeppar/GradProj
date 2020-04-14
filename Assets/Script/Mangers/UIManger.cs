using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour {

    public static UIManger instance;

    [Header("快捷技能")]
    public Image quickSkill1;
    public Image quickSkill2;
 
  
    [Header("道具背包")]
    public BagPanel bagPanel;
    public DescribePanel describePanel;

    [Header("面板物体")]
    public CheatPanel cheatPanel;
    public HPBarPanel hpBarPanel;

    [Header("死亡界面")]
    public GameObject gameOverPanel;

    [Header("提示界面")]
    public GameObject GetItemHelp;
    public GameObject GotItemHelpText;

    private void Awake() {
        instance = this;
    }
 
    public void Init()
    {
        bagPanel.Init();
    }

    public void LoadLevel(int index) {
        SceneManager.LoadScene(index);
    }

    private void Update() {
        hpBarPanel.UpdateHpBar(GameManager.instance.player.HP);
    }
}
