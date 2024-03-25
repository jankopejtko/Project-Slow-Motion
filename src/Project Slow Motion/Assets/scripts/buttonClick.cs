using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonClick : MonoBehaviour
{
    public void LoadScene(string name) 
    {
        if(name == "end" || name == "exit")
        {
            Application.Quit();
        }
        SceneManager.LoadScene(name);
    }
    public void LoadURL(string url) 
    {
        Application.OpenURL(url);
    }
}
