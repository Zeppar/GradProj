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
           if(GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo == null)
            {
                return;
            }
           GameManager.instance.skillActionManger.SendMessage(GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skill.Action);
            
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo == null)
            {
                return;
            }
            GameManager.instance.skillActionManger.SendMessage(GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skill.Action);


        }
    }
    public void Fireball()
    {      
        GameManager.instance.skillParticleCreator.CreateFireball(GameManager.instance.player.attackPoint.position, new Vector2(GameManager.instance.player.transform.GetComponent<Player>().dir, 0), 0.5f, Util.SkillCollection.playerFireBall);
    }
    public void Jinhua()
    {
        GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.green;
        Invoke("JinhuaBack",0.2f);
    }
    public void JinhuaBack()
    {
        GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.instance.player.HP += 10;
    }
}

