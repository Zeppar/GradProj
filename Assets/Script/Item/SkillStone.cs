using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour {
    public SkillInfo skillInfo;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag(Util.TagCollection.playerTag)) {
            UIManger.instance.GetItemHelp.SetActive(true);
            if (Input.GetKey(KeyCode.E)) {
                GameManager.instance.goodManger.AddItemToPanel(GoodInfo.GoodType.Skill, skillInfo.ID);

                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         UIManger.instance.GetItemHelp.SetActive(false);
    }
}
