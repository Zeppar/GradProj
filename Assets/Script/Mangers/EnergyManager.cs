using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager
{
    public float totalEnergy;
    public float maxEnergy;

    public EnergyManager(float total, float max) {
        totalEnergy = total;
        maxEnergy = max;
    }

    public void ChangeEnergy(float value) {
        totalEnergy = Mathf.Max(totalEnergy + value, 0);
    }

    public float GetPercentValue() {
        return totalEnergy / maxEnergy;
    }
}
