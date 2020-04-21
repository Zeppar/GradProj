using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWin : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Util.TagCollection.playerTag))
        {
            GameManager.instance.player.dead = true;
            UIManager.instance.levelUpPanel.ShowPanel();
        }
    }
}
