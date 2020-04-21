using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager
{
    public float curEnemgy;
    public float maxEnergy;

    public EnergyManager(float total, float max) {
        curEnemgy = total;
        maxEnergy = max;
    }

    public void ChangeEnergy(float value) {
        curEnemgy = Mathf.Max(curEnemgy + value, 0);
    }

    public float GetPercentValue() {
        return curEnemgy / maxEnergy;
    }
}
