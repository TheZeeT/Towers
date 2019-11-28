using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 0.2f;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance = 2;
    private float startTime;


    
    void Start()
    {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);

        
        targetPosition = target.transform.position;
        
    }

    
    void Update()
    {
        if(target.gameObject == null)
        {
             Destroy(this.gameObject);
        }

        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
        targetPosition = target.transform.position;
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("this = " + other.gameObject.tag);

        if(other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().LoseHP(40);
            Destroy(this.gameObject);
        }
    }
}
