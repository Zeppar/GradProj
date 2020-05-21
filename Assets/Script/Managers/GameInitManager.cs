using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitManager : MonoBehaviour {

    public Transform spawn;
    private void Start() {
        StartCoroutine(Util.DelayExecute(() => {
            return GameManager.instance != null;
        }, () => {
            GameManager.instance.InitPlayer(spawn.position);
        }));
    }
}
