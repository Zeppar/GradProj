using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    public GameObject Boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Boss.SetActive(true);
    }
}
