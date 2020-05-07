using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public KeyCode keyCode;

    public Sprite keyUp;
    public Sprite keyDwon;

    private TextMesh showText;
    private SpriteRenderer spriteRenderer;
    private Vector3 normalPos;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        showText = GetComponentInChildren<TextMesh>();
        normalPos = showText.transform.position;
    }
    private void Update() {
        if (Input.GetKeyDown(keyCode)) {
            spriteRenderer.sprite = keyDwon;
            showText.transform.position = normalPos + new Vector3(0, -0.12f, 0);
        }
        if (Input.GetKeyUp(keyCode)) {
            spriteRenderer.sprite = keyUp;
            showText.transform.position = normalPos;
        }
    }
}
