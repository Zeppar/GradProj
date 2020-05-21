using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StorySkip : MonoBehaviour {
    public List<string> wordsList;
    public Text showText;

    private int count;
    void Start() {
        StartCoroutine(SetText());

    }
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            count++;
            if (count >= 70) {
                Util.LevelOp.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            count = 0;
        }
    }

    IEnumerator SetText() {
        foreach (var item in wordsList) {
            showText.CrossFadeAlpha(0, 0, true);
            showText.text = item;
            showText.CrossFadeAlpha(1, 1, true);
            yield return new WaitForSeconds(2);
            showText.CrossFadeAlpha(0, 1, true);
            yield return new WaitForSeconds(1.5f);
        }
        Util.LevelOp.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
