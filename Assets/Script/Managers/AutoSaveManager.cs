using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaveManager
{
    public bool hasSaved;
    public Vector3 rebornPos;
    // TODO energy

    public void Reset() {
        hasSaved = false;
        rebornPos = Vector3.zero;
    }

    public void Save(Vector3 pos) {
        hasSaved = true;
        rebornPos = pos;
    }
}
