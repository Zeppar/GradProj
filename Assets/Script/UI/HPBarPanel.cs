using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarPanel : MonoBehaviour {

    public List<Image> heart_list;

    public Sprite heartFull;
    public Sprite heartHelf;
    public Sprite heartNull;
    public void UpdateHpBar(float hp) {
        hp = (int)hp;
        if (hp % 2 == 0) {
            for (int i = 0; i < heart_list.Count; i++) {
                if (i + 1 <= hp / 2) {
                    heart_list[i].sprite = heartFull;
                } else {
                    heart_list[i].sprite = heartNull;
                }
            }
        } else {
            for (int i = 0; i < heart_list.Count; i++) {
                if (i + 1 <= hp / 2) {
                    heart_list[i].sprite = heartFull;
                } else if (i + 1 == (hp / 2) + 1) {
                    heart_list[i].sprite = heartHelf;
                } else {
                    heart_list[i].sprite = heartNull;
                }
            }
        }

    }
}
