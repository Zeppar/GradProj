using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour {

    public SkillInfo skillInfo;
    public bool isEnter = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(Util.TagCollection.playerTag)) {
            isEnter = true;
            UIManager.instance.helpPanel.ShowGetItemTip("按下E键以拾取");
        }

    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(Util.TagCollection.playerTag)) {
            UIManager.instance.helpPanel.HideGetItemTip();
            isEnter = false;
        }
    }



    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && isEnter) {
            GameManager.instance.goodManager.AddGoodInfo(new GoodInfo(skillInfo, 1));
            SoundManager.instance.PlayEffect(Util.ClipNameCollection.getItem);
            GameManager.instance.skillParticleCreator.CreateEffect(transform.position, Util.ObjectItemNameCollection.getItemLightEffect);
            UIManager.instance.helpPanel.ShowOwnItemTip();
            GameManager.instance.skillParticleCreator.CreateEffect(GameManager.instance.player.transform.position, Util.ObjectItemNameCollection.getItemEffect);
            GameManager.instance.levelManager.currentInfo.bagEnable = true;
            Destroy(gameObject);
        }
    }
}
