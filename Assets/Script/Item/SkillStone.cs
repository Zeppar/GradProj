using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour {

    public SkillInfo skillInfo;
    public bool isEnter = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(Util.TagCollection.playerTag)) {
            isEnter = true;
            //UIManager.instance.GetItemHelp.SetActive(true);
            UIManager.instance.helpPanel.ShowGetItemTip();
        }

    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(Util.TagCollection.playerTag)) {
            //UIManager.instance.GetItemHelp.SetActive(false);
            UIManager.instance.helpPanel.HideGetItemTip();
            isEnter = false;
        }
    }



    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && isEnter) {
            GameManager.instance.goodManager.AddGoodInfo(new GoodInfo(skillInfo, 1));
            UIManager.instance.helpPanel.ShowOwnItemTip();
            GameManager.instance.energyManager.ChangeEnergy(20.0f);
            GameManager.instance.skillParticleCreator.CreateEffect(GameManager.instance.player.transform.position, Util.ObjectItemNameCollection.getItemEffect);
            Destroy(gameObject);
        }
    }
}
