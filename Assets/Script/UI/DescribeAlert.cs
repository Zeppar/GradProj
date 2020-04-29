using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescribeAlert : MonoBehaviour
{
    public Text titleText;
    public Text describeText;

    public void Show(string title, string des, Vector3 pos) {
        gameObject.SetActive(true);
        titleText.text = title;
        describeText.text = des;
        transform.position = pos;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
