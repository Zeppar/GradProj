<<<<<<< HEAD:Assets/Script/UI/DescribeAlert.cs
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescribeAlert : MonoBehaviour
{
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class describePanel : MonoBehaviour
{
>>>>>>> origin/v1.0:Assets/Script/UI/describePanel.cs
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
