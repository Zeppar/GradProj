using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStoneCreator : MonoBehaviour
{
    public SkillStone skillStonePerfab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateSkillStone(int _id,Vector2 vector2)
    {
        SkillInfo info = GameManger.instance.skillManager.FindSkillWithID(_id);
        if(info == null)
        {
            return;
        }
        SkillStone ss=  Instantiate<SkillStone>(skillStonePerfab,vector2,transform.rotation);
        ss.skillInfo = info;
    }
}
