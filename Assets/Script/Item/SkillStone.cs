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

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.CompareTag(Util.playerTag)) {
            GameManger.instance.skillManager.AddSkill(skillInfo);
            Destroy(gameObject);
        }
    }
}
