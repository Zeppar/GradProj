using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWin : MonoBehaviour
{

    public WheelTip wheelTip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Util.TagCollection.playerTag))
        {
            wheelTip.StartSaving(3.0f, () =>
            {
                GameManager.instance.player.dead = true;
                UIManager.instance.levelUpPanel.ShowPanel();
            });
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag))
        {
            wheelTip.Stop();
        }
    }

}
