using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        SetHealth(120);
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
    }

    public override void AddToMap()
    {
        Vector2 CurPos = new Vector2(this.transform.position.x, this.transform.position.z);

        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].Tower = this.gameObject;
    }

    public override void Highlight()
    {

        //when selected yellow square will be plased under tower
        SelectorArrow.transform.position = this.gameObject.transform.position + new Vector3 (0,-0.5f,0);
        SelectorArrow.transform.localScale = new Vector3(2.8f, 0.05f, 2.8f);

        SelectorArrow.gameObject.transform.parent = this.gameObject.transform;
        
    }

    

    public override void MoveTo(Vector2 coords)
    {
        Debug.Log("Immovable");
        //do nothing
    }
}
