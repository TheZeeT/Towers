using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Small : TowerBase
{

    public GameObject Projectile;

    bool shooting = false;

    void Start()
    {
        Initialize();
        AddToMap();

        SetHealth(100);
    }

    void Update()
    {

        if (EnemiesInRange.Count > 0 && shooting == false)
        {
            StartCoroutine(Cooldown());
        }

        CheckStatus();

    }

    


    IEnumerator Cooldown()
    {
        shooting = true;
        while (EnemiesInRange.Count > 0)
        {
            GameObject proj = Instantiate(Projectile, this.gameObject.transform.position + new Vector3(0,1,0), this.gameObject.transform.rotation);

            proj.GetComponent<Projectile>().target = EnemiesInRange[0];
            proj.GetComponent<Projectile>().startPosition = this.gameObject.transform.position + new Vector3(0, 1, 0);
            proj.GetComponent<Projectile>().speed = 8.0f;
            proj.GetComponent<Projectile>().damage = 20;

            yield return new WaitForSeconds(1);

            if (EnemiesInRange.Count > 0 && EnemiesInRange[0] == null)
            {
                EnemiesInRange.RemoveAt(0);
                break;
            }
        }

        shooting = false;

        yield break;

    }
   

    public override void MoveTo(Vector2 coords)
    {
        Vector2 location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z);

        if (Mathf.Abs(coords.x - location.x) + Mathf.Abs(coords.y - location.y) <= 1)
        {
            base.MoveTo(coords);
        }
        else
        {
            Debug.Log("to far!");
        }
       
    }



   
}
