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
    public bool isMoving;
    public Rigidbody2D rb;
    private ElfType curType;
    private bool isReady = true;

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

    private void Reset() {
        isMoving = false;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void StartOperation(Vector2 dir) {
        if (!isReady)
            return;
        isReady = false;
        switch (curType) {
            case ElfType.Wind:
                isMoving = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(dir.x * 5.0f, dir.y * 5.0f);
                StartCoroutine(Util.DelayExecute(1.5f, Reset));
                break;
            default:
                break;
        }
    }

    public void SetType(ElfType type) {
        curType = type;
        int t = (int)type;
        spriteRenderer.sprite = elfList[t];
        Color color = new Color(colorList[t].r, colorList[t].g, colorList[t].a, 0.2f);
        particleSystem.startColor = color;
        light.color = colorList[t];
    }

    private void Update() {
        if (GameManager.instance.player == null || isMoving)
            return;

        if (transform.position != GameManager.instance.player.elfPos.position) {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.elfPos.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag) && isMoving) {
            isReady = true;
        }
    }
}
