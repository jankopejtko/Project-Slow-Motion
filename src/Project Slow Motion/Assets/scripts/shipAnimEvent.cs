using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shipAnimEvent : MonoBehaviour
{
    public void loadNextScene(string name) 
    {
        SceneManager.LoadScene(name);
    }
}
