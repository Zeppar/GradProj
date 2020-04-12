using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Enemy
{
    public Transform feetPos;
    public bool onGround = true;

    public ContactFilter2D contactFilter;
    public RaycastHit2D[] resultArr = new RaycastHit2D[16];

    public override void Start() {
        base.Start();
        Begin();
    }

    public override void Begin() {
        contactFilter.useTriggers = false;
        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(LayerMask.GetMask(Util.LayerCollection.groundLayer));
    }

    public virtual void CheckAttackPlayer() {
        AddVelocity(new Vector2(dir * 1.0f, 0));
    }

    public override void UpdateState() {
        //isGrounded = feetCollider.IsTouchingLayers(LayerMask.GetMask(Util.LayerCollection.groundLayer));
        onGround = Physics2D.OverlapCircle(feetPos.position, 2.0f, LayerMask.GetMask(Util.LayerCollection.groundLayer));
    }

    public override void Seek() {//巡逻
        if (!onGround) {
            return;
        }
        anim.SetBool("Walk", true);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position + new Vector3(dir * 5, 0), new Vector2(0, -1), 4);
        int count = rb.Cast(new Vector2(dir * 5, 0), contactFilter, resultArr, 5 + 0.01f);
        if (count != 0 || downHit.collider == null) {
            dir *= -1;
        }
        transform.localScale = new Vector2(dir * scaleMulti, transform.localScale.y);
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * dir, transform.position.y);
    }

    public override void Chase() {//追逐
        if (!onGround) {
            return;
        }
        anim.SetBool("Walk", true);
        if (GameManager.instance.player.transform.position.x > transform.position.x + 0.5f) {
            dir = 1;
        } else if (GameManager.instance.player.transform.position.x < transform.position.x - 0.5f) {
            dir = -1;
        }
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * dir, transform.position.y);
        transform.localScale = new Vector2(dir * scaleMulti, transform.localScale.y);
    }

    public override void Die() {
        base.Die();
        anim.SetBool("Dead", true);
    }

    public void DestorySelf() {
        Destroy(gameObject);
    }
}
