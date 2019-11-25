using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public GameObject SelectorArrow;
    public BoardController Board;

    void Start()
    {
        SelectorArrow = GameObject.Find("SelectorArrow");
        Board = GameObject.Find("PlayBoard").GetComponent<BoardController>();

        //StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.5f);
        Initialize();
    }

    void Initialize()
    {
        SelectorArrow = GameObject.Find("SelectorArrow");
        Board = GameObject.Find("BoardController").GetComponent<BoardController>();
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
