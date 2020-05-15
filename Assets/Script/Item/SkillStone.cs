using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour {

    public SkillInfo skillInfo;
    public bool isEnter = false;
    public bool addSkill;

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
            if (addSkill) {
                GameManager.instance.goodManager.AddGoodInfo(new GoodInfo(skillInfo, 1));
                UIManager.instance.helpPanel.ShowOwnItemTip();
                GameManager.instance.levelManager.currentInfo.bagEnable = true;
            }
            SoundManager.instance.PlayEffect(Util.ClipNameCollection.getItem);
            UIManager.instance.ShowGetItemUI(20);
            GameManager.instance.energyManager.StartIncreate(20.0f);
            GameManager.instance.skillParticleCreator.CreateEffect(GameManager.instance.player.transform.position, Util.ObjectItemNameCollection.getItemEffect);
            Destroy(gameObject);
        }
    }
}
