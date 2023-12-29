using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class lookAtPlayerCamera : MonoBehaviour
{
    Camera playerCamera;
    private void Start()
    {
        playerCamera = GameObject.FindGameObjectsWithTag("player")[0].GetComponentInChildren<Camera>();
    }
    void Update()
    {
        if (playerCamera == null)
        {
            Debug.Log("camera not found");
        }
        else 
        {
            Vector3 targetPostition = new Vector3(playerCamera.transform.position.x, this.transform.position.y, playerCamera.transform.position.z);
            transform.LookAt(targetPostition);
        }
    }
}
