using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))       
            Application.Quit();       
        if(Input.GetKeyDown(KeyCode.LeftWindows))         
            Time.timeScale = 0f;                           
        if(Input.GetKeyDown(KeyCode.LeftControl))
            Time.timeScale = 1f;
    }

}
