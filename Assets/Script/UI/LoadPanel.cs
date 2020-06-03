using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    public Slider slider;
    public Text doneText;
    public AsyncOperation operation;
    private void Start()
    {
        NextLeve();
    }
    public void NextLeve()
    {
        StartCoroutine(LoadLevel());
    }
    public IEnumerator LoadLevel()
    {
        Debug.Log("HI");
        operation = SceneManager.LoadSceneAsync(Util.Level.nextLevelID);
        operation.allowSceneActivation = false;
        int i = 0;
        while (!operation.isDone)
        {

            //Debug.Log(operation.progress);
            slider.value = operation.progress;
            doneText.gameObject.SetActive(true);
            if (operation.progress >= 0.9f)
            {
                slider.value = 1;
                yield return null;
            }
        }
      //  operation.allowSceneActivation = true;
    }
    public void Update()
    {
        Debug.Log(0);
        if (operation.progress >= 0.9f)
        {
            Debug.Log(1);
            if (Input.anyKeyDown)
            {
                Debug.Log(2);
                operation.allowSceneActivation = true;
            }
        }
    }
}



