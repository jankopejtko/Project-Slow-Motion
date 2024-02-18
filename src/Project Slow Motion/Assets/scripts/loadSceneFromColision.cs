using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneFromColision : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] string objectTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == objectTag)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
