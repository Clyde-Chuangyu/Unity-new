using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject win;
    
    void Start()
    {
        // 确保开始时win界面是隐藏的
        win.SetActive(false);
    }

    // 这个方法被boss死亡动画的Animation Event调用
    public void ShowWin()
    {
        win.SetActive(true);
        Time.timeScale = 0;
    }

    }