using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class showBulletProgressBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] shoot pistolValues;
    int numOfBulletsMax;
    int numOfBulletsLeft;

    private void Start()
    {
        numOfBulletsMax = pistolValues.BulletNumberMAX;
    }
    private void Update()
    {
        numOfBulletsLeft = pistolValues.BulletNumber;
        slider.value = (numOfBulletsLeft) / (numOfBulletsMax);
    }
}
