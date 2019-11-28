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


    public float speed = 0.2f;
    public int damage;
    public GameObject target;
    public Vector3 startPosition = new Vector3(0,0,0);
    public Vector3 targetPosition = new Vector3(5,5,5);

    private float distance;
    private float startTime;

    void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / 2);
    }
}
