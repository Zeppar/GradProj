using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum ElfType {
    Eye = 0,
    Fire,
    Heart,
    EnterNight,
    Skull,
    Sun,
    Tree,
    RefreshCD,
    StrengthMovement
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
    public ElfType curType;
    private bool isReady = true;
    private float timer = 0;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        SetType(ElfType.StrengthMovement);
    }

    private void Reset() {
        isReady = true;
        isMoving = false;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        timer = 0;
    }

    private void FixedUpdate() {
        switch(curType) {
            case ElfType.StrengthMovement:
                if (isMoving)
                    timer += Time.fixedDeltaTime;
                break;
            default:
                break;
        }
    }

    public void StartOperation(Vector2 dir) {
        if (!isReady)
            return;
        isReady = false;
        switch (curType) {
            case ElfType.StrengthMovement:
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
        switch (curType) {
            case ElfType.EnterNight:
                if(Input.GetKey(KeyCode.E)) {
                    Debug.Log("Enter Night Mode");
                }
                break;
            case ElfType.RefreshCD:
                if (Input.GetKeyDown(KeyCode.E)) {
                    Debug.Log("Refresh CD");
                }
                break;
            case ElfType.StrengthMovement:
                if (GameManager.instance.player == null || isMoving)
                    return;
                break;
            default:
                break;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                SetType(ElfType.Sun);
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                SetType(ElfType.Tree);
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                SetType(ElfType.RefreshCD);
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                SetType(ElfType.StrengthMovement);
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                SetType(ElfType.Eye);
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                SetType(ElfType.Fire);
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                SetType(ElfType.Heart);
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                SetType(ElfType.EnterNight);
            } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
                SetType(ElfType.Skull);
            }
        }

        if (transform.position != GameManager.instance.player.elfPos.position) {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.elfPos.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (timer < 0.1f)
            return;
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag) && isMoving) {
            Debug.Log("OnTriggerEnter2D");
            Reset();
            ElfOperation();
        }
    }

    private void ElfOperation() {
        switch(curType) {
            case ElfType.StrengthMovement:
                GameManager.instance.player.ResetDashCD();
                break;
            default:
                Debug.Log("TODO");
                break;
        }
    }
}
