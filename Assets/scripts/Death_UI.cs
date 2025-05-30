using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Death_UI : MonoBehaviour
{
    public void DeathUI()
    {
        SceneManager.LoadScene(2);
    }
}