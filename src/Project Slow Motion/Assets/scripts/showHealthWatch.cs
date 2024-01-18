using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class showHealthWatch : MonoBehaviour
{
    private entityStats es;
    public Text healthText;
    private void Start()
    {
        es = GameObject.FindGameObjectWithTag("player").GetComponent<entityStats>();
    }
    void Update()
    {
        healthText.text = es.Health + " HP";
        if (es.Health > (es.MaxHealth * 0.75))
        {
            healthText.color = Color.green;
        }
        else if (es.Health < (es.MaxHealth * 0.5)) 
        {
            healthText.color = Color.yellow;
        }
        else if (es.Health < (es.MaxHealth * 0.25)) 
        {
            healthText.color = Color.red;
        }
        //pouze pro test, následnì bude uklizeno v jiném souboru
        if(es.Health <= 0) 
        {
            SceneManager.LoadScene("menu");
        }
    }
}
