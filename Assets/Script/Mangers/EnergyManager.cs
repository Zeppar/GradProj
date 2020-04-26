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

    public float GetEnemyAttackRatio() {
        // 75 % 以上 0.9   50 % 以上 1.0  50% 一下 加成 最高两倍 50 - 1 0 -> 2
        float percent = GetPercentValue();
        if(percent > 0.75f) {
            return 0.9f;
        } else if(percent > 0.5f) {
            return 1.0f;
        } else {
            return (0.5f - percent) * 2 + 1f;
        }
    }
}
