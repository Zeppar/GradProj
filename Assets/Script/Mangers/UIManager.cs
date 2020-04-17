using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

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
    public HelpPanel helpPanel;

    [Header("血量背景")]
    public Transform hpPanelParent;

    [Header("通用")]
    public Transform goodItemMidParent;

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

    public void ShowHPUI(Enemy enemy, int hp) {
        var hpUI = ObjectPool.instance.GetItem(Util.ObjectItemNameCollection.EnemyHpCanvas);
        hpUI.transform.SetParent(hpPanelParent, false);
        hpUI.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1, 0)); 
        hpUI.GetComponent<EnemyHpCanvas>().ShowHPUI(hp);
    }

    private void Update() {
        hpBarPanel.UpdateHpBar(GameManager.instance.player.HP);
    }
}
