using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class target : MonoBehaviour
{
    public entityStats entity;
    public string tag = "bullet";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
        {
            entity.Health = entity.Health - other.gameObject.GetComponent<bullet>().force;
            Destroy(other.gameObject);
        }
    }
}
