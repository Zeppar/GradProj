using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosion : MonoBehaviour
{
    private IEnumerator Start() {
        yield return new WaitForSecondsRealtime(4.0f);
        Destroy(gameObject);
    }
}
