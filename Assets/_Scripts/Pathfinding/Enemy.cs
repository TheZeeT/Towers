using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{


    public GameObject TargetTower;

    public GameObject HealthBar;

    
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
        this.gameObject.GetComponent<Unit>().speed = _speed;
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
        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void HealthBarAdjustment()
    {
        if(Health <= 0)
        {
            HealthBar.SetActive(false);
        }

        HealthBar.gameObject.transform.localScale = new Vector3(1, (1.0f / MaxHealth) * Health, 1);

        float r = 0.3f + (1.0f / MaxHealth / 2) * Health;
        Color nc = new Color(Mathf.Clamp(r,0,1), 0.2f, 0.0f);
        HealthBar.gameObject.GetComponentInChildren<MeshRenderer>().material.color = nc;

    }

   
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Tower"))
        {
            collision.gameObject.GetComponentInParent<TowerBase>().Health -= Health;
            Destroy(this.gameObject);
        }
    }
}