using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelManager : MonoBehaviour
{

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LevelUp()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        if (GameManager.instance != null)
        {
            GameManager.instance.levelManager.EnterNextLevel();
        }
    }
    public void ReLoadLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

}
