using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
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
        operation = SceneManager.LoadSceneAsync(Util.Level.nextLevelID);
        operation.allowSceneActivation = false;
        int i = 0;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return null;
            }
        }
      //  operation.allowSceneActivation = true;
    }
    public void Update()
    {
        if (operation.progress >= 0.9f)
        {
                operation.allowSceneActivation = true;
            }
        }
    }




