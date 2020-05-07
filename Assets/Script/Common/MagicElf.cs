using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum ElfType {
    Eye = 0,
    Fire,
    Heart,
    Moon,
    Skull,
    Sun,
    Tree,
    Water,
    Wind
}

public class MagicElf : MonoBehaviour
{
    public static MagicElf instance = null;

    public List<Sprite> elfList;
    public List<Color> colorList;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem particleSystem;
    public Light2D light;
    public float speed;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        SetType(ElfType.Wind);
    }

    public void SetType(ElfType type) {
        int t = (int)type;
        spriteRenderer.sprite = elfList[t];
        Color color = new Color(colorList[t].r, colorList[t].g, colorList[t].a, 0.2f);
        particleSystem.startColor = color;
        light.color = colorList[t];
    }

    private void Update() {
        Debug.Log(transform.position + " - " + GameManager.instance.player.elfPos.position);
        if (transform.position != GameManager.instance.player.elfPos.position) {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.elfPos.position, speed * Time.deltaTime);
        }
    }
}
