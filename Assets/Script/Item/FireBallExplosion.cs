using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosion : ObjectPoolItem
{
    public override void OnEnable() {
        SoundManager.instance.PlayEffect(Util.ClipNameCollection.explosion);
        base.OnEnable();
    }
}
