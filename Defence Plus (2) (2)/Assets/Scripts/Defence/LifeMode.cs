using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeMode : MonoBehaviour
{

    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
        }
    }
}
