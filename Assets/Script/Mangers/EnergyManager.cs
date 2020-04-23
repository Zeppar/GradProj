using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager
{
    public float curEnemgy;
    public float maxEnergy;

    public bool inEnergyInCrease;
    public float increaseValue;

    public void StartIncreate(float val) {
        inEnergyInCrease = true;
        increaseValue = val;
    }

    public EnergyManager(float total, float max) {
        curEnemgy = total;
        maxEnergy = max;
    }

    public void ChangeEnergy(float value) {
        curEnemgy = Mathf.Max(curEnemgy + value, 0);
    }

    public void IncreaseEnergy(float value) {
        curEnemgy = Mathf.Max(curEnemgy + value, 0);
        increaseValue -= value;
        if(Mathf.Approximately(increaseValue, 0) || increaseValue < 0) {
            increaseValue = 0;
            inEnergyInCrease = false;
        }
    }

    public float GetPercentValue() {
        return curEnemgy / maxEnergy;
    }
}
