using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {
    public Button retryBtn;
    public Button quitBtn;
    public Button rebornBtn;

    private void OnEnable() {
        rebornBtn.gameObject.SetActiveFast(GameManager.instance.autoSaveManager.hasSaved);
    }

    void Start() {
        retryBtn.onClick.AddListener(() => {
            GameManager.instance.ReLoadLevel();
            //gameObject.SetActiveFast(false);
            //GameManager.instance.InitPlayer(GameManager.instance.spawn.position);
        });
        quitBtn.onClick.AddListener(() => {
            GameManager.instance.LoadLevel(0);
        });
        rebornBtn.onClick.AddListener(() => {
            if (!GameManager.instance.autoSaveManager.hasSaved)
                return;
            gameObject.SetActiveFast(false);
            GameManager.instance.InitPlayer(GameManager.instance.autoSaveManager.rebornPos);
        });
    }

}
