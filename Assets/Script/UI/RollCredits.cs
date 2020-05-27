using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollCredits : MonoBehaviour
{
    public float speed;

    public float endYPos;

    public Button button;

    
    void Update()
    {
        if(transform.position.y <= endYPos)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime));
        }
        else
        {        
            button.gameObject.SetActive(true);
        }
    }
}
