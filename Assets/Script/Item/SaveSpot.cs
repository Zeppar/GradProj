using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SaveSpot : MonoBehaviour
{
    public Light2D light;

    private void Start() {
        light.color = Util.ColorFromString("#808080", 1.0f);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            light.color = Util.ColorFromString("#FFFFFF", 1.0f);
            GameManager.instance.autoSaveManager.Save(GameManager.instance.player.transform.position);
        }        
    }

}
