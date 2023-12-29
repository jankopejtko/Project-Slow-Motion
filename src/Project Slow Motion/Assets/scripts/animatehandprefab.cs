using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class animatehandprefab : MonoBehaviour
{
    public InputActionReference grip;
    public InputActionReference trigger;

    private Animator handAnimator;
    private float gripValue;
    private float triggerValue;

    private void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        animateGrip();
        animateTrigger();
    }

    private void animateTrigger()
    {
        triggerValue = trigger.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
    }

    private void animateGrip()
    {
        gripValue = grip.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
