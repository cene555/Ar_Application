using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float deleteTime = 2f; 

    void Update()
    {
        Invoke("DeleteObject", deleteTime);
    }

    private void DeleteObject()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Turret")
        {
            Destroy(other.gameObject);
        }
    }
    }
