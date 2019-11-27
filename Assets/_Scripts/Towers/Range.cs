using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    //void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log("sdasd");
    //}


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("IN RAGE!");
        
    }
}
