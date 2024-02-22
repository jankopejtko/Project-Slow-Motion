using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class speedPlayer : MonoBehaviour
{
    [SerializeField] ActionBasedContinuousMoveProvider provider;
    float normalSpeed;
    bool superSpeedEnebled;
    void Start()
    {
        normalSpeed = provider.moveSpeed;
    }
    public void speedWalk() 
    {
        provider.moveSpeed = normalSpeed * 2;
    }
    public void resetSpeedTime()
    {
        provider.moveSpeed = normalSpeed;
    }
}
