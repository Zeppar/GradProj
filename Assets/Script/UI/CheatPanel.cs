using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatPanel : MonoBehaviour
{
    public InputField hpField;
    public InputField attackField;
    public InputField jumpField;
    public InputField speedField;
    public InputField fallField;
    public InputField dashSpeedField;
    public InputField dashCDField;

    private void Awake()
    {
        hpField.text = GameManager.instance.player.maxHP.ToString();
        attackField.text = GameManager.instance.player.attack.ToString();
        jumpField.text = GameManager.instance.player.jumpForce.ToString();
        speedField.text = GameManager.instance.player.speed.ToString();
        fallField.text = GameManager.instance.player.fallForce.ToString();
        dashSpeedField.text = GameManager.instance.player.dashSpeed.ToString();
        dashCDField.text = GameManager.instance.player.dashCD.ToString();
    }

    public void Apply()
    {
        GameManager.instance.player.maxHP = int.Parse(hpField.text);
        GameManager.instance.player.attack = int.Parse(attackField.text);
        GameManager.instance.player.jumpForce = float.Parse(jumpField.text);
        GameManager.instance.player.speed = float.Parse(speedField.text);
        GameManager.instance.player.fallForce = float.Parse(fallField.text);
        GameManager.instance.player.dashSpeed = float.Parse(dashSpeedField.text);
        GameManager.instance.player.dashCD = float.Parse(dashCDField.text);
    }
    public void MaxHP()
    {
        GameManager.instance.player.HP = GameManager.instance.player.maxHP;
    }
    public void MaxLight() { 
        GameManager.instance.energyManager.StartIncreate(1000);
    }
    public void NextLevel()
    {
        GameManager.instance.LevelUp();
    }
}
