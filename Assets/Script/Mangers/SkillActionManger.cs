using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActionManger : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
           if(GameManger.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo == null)
            {
                return;
            }
           GameManger.instance.skillActionManger.SendMessage(GameManger.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skill.Action);
            
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (GameManger.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo == null)
            {
                return;
            }
            GameManger.instance.skillActionManger.SendMessage(GameManger.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skill.Action);


        }
    }
    public void Fireball()
    {      
        GameManger.instance.skillParticleCreator.CreateFireball(GameManger.instance.playerScript.AttackPoint.position, new Vector2(GameManger.instance.player.transform.GetComponent<Player>().dir, 0));
    }
    public void Jinhua()
    {
        GameManger.instance.player.GetComponent<SpriteRenderer>().color = Color.green;
        Invoke("JinhuaBack",0.2f);
    }
    public void JinhuaBack()
    {
        GameManger.instance.player.GetComponent<SpriteRenderer>().color = Color.white;
        GameManger.instance.playerScript.HP += 10;
    }
}

