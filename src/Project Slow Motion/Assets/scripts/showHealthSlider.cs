using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showHealthSlider : MonoBehaviour
{
    [SerializeField] entityStats entity;
    [SerializeField] Slider healthBar;
    void Update()
    {
        Debug.Log(entity.Health / entity.MaxHealth);
        healthBar.value = (entity.Health / entity.MaxHealth);
    }
}
