using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{


    public GameObject TargetTower;

    public GameObject HealthBar;

    public float speed = 1;
    public int MaxHealth = 100;
    public int Health = 100;

    public float initializationTime;
    public bool WAIT = false;


    void Update()
    {
        if(Input.GetKeyDown("x"))
        {
            LoseHP(10);
        }
    }
    

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void SetHealh(int _health)
    {
        MaxHealth = _health;
        Health = _health;
    }

    public void GoAndAttack()
    {
        initializationTime = Time.timeSinceLevelLoad;
        this.gameObject.GetComponent<Unit>().MoveToTargetPos(TargetTower.transform.position);
    }

    public void LoseHP(int _damage)
    {
        Health -= _damage;
        HealthBarAdjustment();
    }

    void HealthBarAdjustment()
    {
        if(Health <= 0)
        {
            HealthBar.SetActive(false);
        }

        HealthBar.gameObject.transform.localScale = new Vector3(1, (1.0f / MaxHealth) * Health, 1);
    }

   
}