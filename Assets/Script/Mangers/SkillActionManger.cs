using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillActionManger : MonoBehaviour
{
    private float lastActionA = 0;
    private float lastActionB= 0;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo != null)
        {
            int cdA = GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skill.CD;
            UIManger.instance.quickSkill1.GetComponentInChildren<GoodItem>().Mask.fillAmount =1-( (Time.time - lastActionA) / cdA);
        }
        if (GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo != null)
        {
            int cdB = GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skill.CD;
            UIManger.instance.quickSkill2.GetComponentInChildren<GoodItem>().Mask.fillAmount = 1-((Time.time - lastActionB) / cdB);
        }

         
        if (Input.GetKeyDown(KeyCode.K))
        {           
           if (GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo == null)
            {
                return;
            }
            int cd = GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skill.CD;        
            if (Time.time - lastActionA > cd)
            {
                GameManager.instance.skillActionManger.SendMessage(GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skill.Action, GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skill);
                lastActionA = Time.time;
            }
            else
            {
                print("Nono");
            }

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo == null)
            {
                return;
            }
            int cd = GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skill.CD;
       
            if (Time.time - lastActionB > cd)
            {
                GameManager.instance.skillActionManger.SendMessage(GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skill.Action, GameManager.instance.goodManger.goodInfoList[UIManger.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skill);
                lastActionB = Time.time;
            }
       
        }
    }
    public void Fireball(SkillInfo info)
    {
        GameManager.instance.skillParticleCreator.CreateFireball(GameManager.instance.player.attackPoint.position, new Vector2(GameManager.instance.player.transform.GetComponent<Player>().dir, 0), 0.5f, Util.SkillCollection.playerFireBall,info.Value,Util.TagCollection.enemyTag,Util.EffectCollection.playerFireBallExplosion);
    }
    public void Jinhua(SkillInfo info)
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


