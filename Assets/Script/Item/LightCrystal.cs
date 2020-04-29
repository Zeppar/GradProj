using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCrystal : MonoBehaviour
{
    public float lightValue;
    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            GameManager.instance.energyManager.StartIncreate(lightValue);
            Destroy(gameObject);
        }
    }
}
