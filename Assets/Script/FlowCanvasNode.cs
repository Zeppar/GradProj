using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlowCanvas;
using FlowCanvas.Nodes;
using ParadoxNotion.Design;

[Category("Skill")]
public class FireBall : CallableActionNode
{
    public override void Invoke()
    {
        GameManger.instance.skillParticleCreator.CreateFireball(GameManger.instance.playerScript.AttackPoint.position, new Vector2(GameManger.instance.player.transform.GetComponent<PlayerController>().dir, 0));
    }

    
}

[Category("Skill")]
public class Wait : CallableFunctionNode<float,float,float>
{
    public override float Invoke(float CD,float Firsttime)
    {
        Debug.Log("Start");
        if (Time.time - Firsttime >= CD)
        {
            Debug.Log("True");

            return Time.time;
        }
        else { return Firsttime; }
        
    }
  
}

[Category("Skill")]
public class GetTime : CallableFunctionNode<float>
{

    public override float Invoke()
    {
        return Time.time;
    }
}

