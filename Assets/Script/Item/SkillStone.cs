using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour
{
  
   
  public SkillInfo skillInfo;
    private void Awake()
    {
     
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
      
        if (collision.CompareTag(Util.playerTag)) {
            UIManger.instance.GetItemHelp.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                GameManger.instance.goodManger.AddItemToPanel(GoodInfo.GoodType.Skill, skillInfo.ID);

                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         UIManger.instance.GetItemHelp.SetActive(false);
    }
}
