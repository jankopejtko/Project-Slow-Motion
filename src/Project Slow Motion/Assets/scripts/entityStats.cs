using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityStats : MonoBehaviour
{
    [Range(10, 500)]
    [SerializeField] int maxHealth;
    public bool kill;
    public float MaxHealth { get { return maxHealth; } }
    private float health;
    public float Health { get { return health; } set { health = value; } }
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        if (kill)
        {
            health = 0;
        }
    }
}