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
       StartCoroutine(LoadLevel());
    }
    public IEnumerator LoadLevel()
    {
        Debug.Log("HI");
        operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        operation.allowSceneActivation = false;

       while (!operation.isDone)
        {
            slider.value = operation.progress;

            if(operation.progress >= 0.9f)
            {
                slider.value = 1;
                doneText.gameObject.SetActive(true);
                if (Input.anyKeyDown)
                {
                   operation.allowSceneActivation = true;
                   // yield return null;
                    yield return operation;
                }
            }
      }
     
    }
}
