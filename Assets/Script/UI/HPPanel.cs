using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPPanel : MonoBehaviour
{
    public Image green;
    Player player;
    void Update()
    {
        player = GameManager.instance.player;
        if (player == null)
        {
            return;
        }
        green.fillAmount = player.HP / player.maxHP;
    }
}
