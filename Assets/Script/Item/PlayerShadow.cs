using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public float deltaAlpha = 0.05f;
    public GameObject explosionEffect;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf());
        StartCoroutine(DestroyEffect());
    }

    IEnumerator DestroySelf() {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);
        Color color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        while (true) {
            yield return new WaitForEndOfFrame();
            sr.color = Color.Lerp(sr.color, color, deltaAlpha);
            if (sr.color.a < 0.01f)
                Destroy(gameObject);
        }
    }

    IEnumerator DestroyEffect() {
        yield return new WaitForSecondsRealtime(2.0f);
        Destroy(explosionEffect);
    }
}
