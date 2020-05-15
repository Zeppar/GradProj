using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystalType {
    Light = 0,
    HP
}

public class Crystal : MonoBehaviour
{
    public CrystalType type;
    public float value;
    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            bool playMuchEffect = false;
            if (type == CrystalType.Light) {
                GameManager.instance.energyManager.StartIncreate(value);
                playMuchEffect = value >= 10.0f;
            } else {
                GameManager.instance.player.HP += value;
                playMuchEffect = value >= 5.0f;
            }
            SoundManager.instance.PlayEffect(playMuchEffect ? Util.ClipNameCollection.getMuchLight : Util.ClipNameCollection.getLittleLight);
            Destroy(gameObject);
        }
    }
}
