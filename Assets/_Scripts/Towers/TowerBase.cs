using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public GameObject SelectorArrow;
    public BoardController Board;

    void Start()
    {
        SelectorArrow = GameObject.Find("SelectorArrow"); //object that will be used to highlight towers 
        Board = GameObject.Find("PlayBoard").GetComponent<BoardController>(); //the game board script

        //adding this tower to map array so it will be known about
        Vector2 CurPos = new Vector2(this.transform.position.x, this.transform.position.z);

        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].walkable = false;
        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].Tower = this.gameObject;
    }


    public void Highlight()
    {
        SelectorArrow.transform.position = this.gameObject.transform.position + new Vector3 (0,-0.5f,0);

        SelectorArrow.gameObject.transform.parent = this.gameObject.transform;
        
    }

    public virtual void MoveTo(Vector2 coords)
    {
        Vector2 CurPos = new Vector2(this.transform.position.x, this.transform.position.z);

        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].walkable = true;
        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].Tower = null;
        
        this.gameObject.transform.position = new Vector3(coords.x, 1.5f, coords.y);

        Board.ActualMap[(int)coords.x, (int)coords.y].walkable = false;
        Board.ActualMap[(int)coords.x, (int)coords.y].Tower = this.gameObject;

        Debug.Log("Mov: " + CurPos.x + "/" + CurPos.y + " to: " + coords.x + "/" + coords.y);
    }

   
}
