using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class slowTime : MonoBehaviour
{
    [SerializeField] ActionBasedContinuousMoveProvider provider;
    [SerializeField] ActionBasedSnapTurnProvider turnProvider;

    [SerializeField] CharacterController playerCh;
    float timeScale;
    float maxSpeed;
    [SerializeField] float minSpeed;

    private void Start()
    {
        maxSpeed = provider.moveSpeed;
    }
    void Update()
    {
        timeScale = (playerCh.velocity.magnitude - minSpeed) / (maxSpeed-minSpeed);
        if(timeScale < minSpeed)
        {
            timeScale = minSpeed;
        }
        if(timeScale >= 1) 
        {
            timeScale = 1;
        }
        turnProvider.debounceTime = timeScale / 3;
        Time.timeScale = timeScale;
    }
}
