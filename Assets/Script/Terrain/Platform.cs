using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatFormType {
    Move = 0,
    Obstruct
}

public class Platform : MonoBehaviour
{
    public Transform leftPos;
    public Transform rightPos;
    public PlatFormType type = PlatFormType.Move;
    public float speed;

    private Vector3 targetPos;
    private bool isInLeftPos;

    private void Start() {
        targetPos = rightPos.position;
    }

    private void Update() {
        if (Vector2.Distance(transform.position, targetPos) < 0.1f) {
            isInLeftPos = !isInLeftPos;
            targetPos = isInLeftPos ? leftPos.position : rightPos.position;
        } else {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(type == PlatFormType.Obstruct) {
            return;
        }
        if(collision.gameObject.CompareTag(Util.TagCollection.playerTag) && GameManager.instance.player.transform.parent == null) {
            GameManager.instance.player.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (type == PlatFormType.Obstruct) {
            return;
        }
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag) && GameManager.instance.player.transform.parent != null) {
            GameManager.instance.player.transform.parent = null;
        }
    }

}
