using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public KeyCode keyCode;

    public Sprite keyUp;
    public Sprite keyDwon;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            spriteRenderer.sprite = keyDwon;
        }
        if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.sprite = keyUp;
        }
    }
}
