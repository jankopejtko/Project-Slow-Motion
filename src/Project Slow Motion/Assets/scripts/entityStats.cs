using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityStats : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    private float health;
    public float Health { get { return health; } set { health = value; } }
    private void Start()
    {
        health = maxHealth;
    }
}