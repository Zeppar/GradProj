using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour {
    public Button NextLevel;
    public Button BackTitle;
    private void Start() {
        NextLevel.onClick.AddListener(() => { GameManager.instance.LevelUp(); });
        NextLevel.onClick.AddListener(() => { gameObject.SetActive(false); });
        BackTitle.onClick.AddListener(() => { Util.LevelOp.LoadLevel(0); });
    }
    public void ShowPanel() {
        gameObject.SetActive(true);
    }
}
