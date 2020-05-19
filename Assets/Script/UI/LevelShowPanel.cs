using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelShowPanel : MonoBehaviour
{
  public LevelShow levelImage1;
  public LevelShow levelImage2;
  public LevelShow levelImage3;
    public List<LevelShow> levelShows = new List<LevelShow>();

  public void Init(int level)
    {
        levelShows.Add(levelImage1);
        levelShows.Add(levelImage2);
        levelShows.Add(levelImage3);
        int a = 0;
        for (int i = 0; i < level; i++)
        {
            levelShows[a].AddLevel();
            a++;
            if(a > 2) { a = 0; }
         
        }
        }
    }


