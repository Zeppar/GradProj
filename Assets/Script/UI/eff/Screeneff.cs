using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screeneff : MonoBehaviour
{
    //使用 代码 https://blog.csdn.net/weixin_33950757/java/article/details/83896500
    private GameObject image; //设置图片
    private RawImage rawImage;//设置rawimage
    public float speed = 3f;
    //屏幕是否要逐渐清晰(默认是需要的)
    private bool isclear = true;
    //屏幕是否需要逐渐变暗(默认是不需要的)
    private bool isblack = false;

    public static Screeneff instance;//实例
    public void Awake()
    {
        instance = this;
        image = this.gameObject;
        rawImage = this.GetComponent<RawImage>();
    }

    /// <summary>
    /// 淡入效果
    /// </summary>
    public void fadetoClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, speed * Time.deltaTime);
    }
    /// <summary>
    /// 淡出效果
    /// </summary>
    public void fadetoBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, speed * Time.deltaTime);
    }

    public void SceneToClear()
    {
        fadetoClear();
        if (rawImage.color.a < 0.05f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            isclear = false;
        }
    }
    public void SceneToBlack()
    {
        rawImage.enabled = true;
        fadetoBlack();
        if (rawImage.color.a > 0.95f)
        {
            rawImage.color = Color.black;
            isblack = true;
        }
    }

    /// <summary>
    /// 设置场景的淡入
    /// </summary>
    public void setSceneToClear()
    {
        isclear = true;
        isblack = false;
    }

    /// <summary>
    /// 设置场景的淡出
    /// </summary>
    public void setSceneToClean()
    {
        isclear = false;
        isblack = true;
    }

    void Update()
    {

        if (isclear)
        {
            SceneToClear();
        }
        else if (isblack)
        {
            SceneToBlack();
        }

    }

}
