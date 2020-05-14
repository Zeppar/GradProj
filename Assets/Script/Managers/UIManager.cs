﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
  
    [Header("道具背包")]
    public BagPanel bagPanel;
    public DescribeAlert describeAlert;

    [Header("面板物体")]
    public HPBarPanel hpBarPanel;

    [Header("死亡界面")]
    public GameObject gameOverPanel;

    [Header("提示界面")]
    public HelpPanel helpPanel;

    [Header("血量背景")]
    public Transform hpPanelParent;

    [Header("通用")]
    public Transform goodItemMidParent;
    public AttackScreenEffect screenEffect;
    public ComboPanel comboPanel;
    public EnergyPanel energyPanel;
    public LevelUpPanel levelUpPanel;
    public DashFlag dashFlag;
    public QuickSkillPanel quickSkillPanel;
    public PausePanel pausePanel;
    public KeySetPanel keySetPanel;
    public CheatPanel cheatPanel;

    public CodePanel codePanel;

    private void Awake() {
        instance = this;
    }
 
    public void Init()
    {
        bagPanel.Init();
        cheatPanel.Init();
    }

    public void ShowHPUI(Enemy enemy, int hp) {
        var hpUI = ObjectPool.instance.GetItem(Util.ObjectItemNameCollection.enemyHpCanvas);
        hpUI.transform.SetParent(hpPanelParent, false);
        hpUI.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1, 0)); 
        hpUI.GetComponent<EnemyHpCanvas>().ShowHPUI(hp);
    }

    private void Update() {
      //  hpBarPanel.UpdateHpBar(GameManager.instance.player.HP);
        dashFlag.SetValue(GameManager.instance.player.GetDashCDPercent());
        dashFlag.gameObject.SetActiveFast(GameManager.instance.levelManager.currentInfo.dashEnable);
        quickSkillPanel.gameObject.SetActiveFast(GameManager.instance.levelManager.currentInfo.bagEnable);
    }
}
