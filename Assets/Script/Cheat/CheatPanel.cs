using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatPanel : MonoBehaviour
{
    public InputField idField;
    
   public void AddItem()
    {
        GameManger.instance.goodManger.AddItemToPanel(GoodInfo.GoodType.Skill,int.Parse(idField.text));       
    }
}
