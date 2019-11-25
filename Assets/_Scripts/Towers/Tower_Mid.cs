using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Mid : TowerBase
{
    public override void MoveTo(Vector2 coords)
    {
        Vector2 location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z);

        if (Mathf.Abs(coords.x - location.x) + Mathf.Abs(coords.y - location.y) <= 2)
        {
            //Debug.Log("x " + coords.x + " / " + location.x + " y : " + coords.y + " / " + location.y);
            base.MoveTo(coords);
        }
        else
        {
            Debug.Log("to far!");
        }


    }
}
