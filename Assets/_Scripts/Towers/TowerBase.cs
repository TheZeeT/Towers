using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public GameObject SelectorArrow;

    void Start()
    {
        //SelectorArrow = GameObject.Find("SelectorArrow");
    }

    public void Highlight()
    {
        SelectorArrow.transform.position = this.gameObject.transform.position + new Vector3 (0,-0.5f,0);

        SelectorArrow.gameObject.transform.parent = this.gameObject.transform;
        
    }

    public virtual void MoveTo(Vector2 coords)
    {
        this.gameObject.transform.position = new Vector3(coords.x, 1.5f, coords.y);
    }

   
}
