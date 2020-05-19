using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SaveSpot : MonoBehaviour
{
    public Light2D light;
    public Sprite destroySp;
    public Sprite activeSp;
    public SpriteRenderer spriteRenderer;
    public WheelTip wheelTip;
    
    private void Start() {
        light.color = Util.ColorFromString("#808080", 1.0f);
        spriteRenderer.sprite = destroySp;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            wheelTip.StartSaving(1.0f, () => {
                light.color = Util.ColorFromString("#FFFFFF", 1.0f);
                spriteRenderer.sprite = activeSp;
                GameManager.instance.autoSaveManager.Save(GameManager.instance.player.transform.position);
            });
        }        
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            wheelTip.Stop();
        }
    }

}
