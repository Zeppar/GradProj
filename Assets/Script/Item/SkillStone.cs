using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour {
    public SkillInfo skillInfo;
    public bool isEnter = false;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag(Util.TagCollection.playerTag)) {
            isEnter = true;
            UIManger.instance.GetItemHelp.SetActive(true);          
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         UIManger.instance.GetItemHelp.SetActive(false);
        isEnter = false;
    }

   

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && isEnter)
        {
            GameManager.instance.goodManger.AddItemToPanel(GoodInfo.GoodType.Skill, skillInfo.ID);

            Destroy(gameObject);
        }

    }
}
