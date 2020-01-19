using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour

    /*
    Base for all other tower scripts
        
     */
{
    public GameObject SelectorArrow;
    public BoardController Board;

    public List<GameObject> EnemiesInRange;

    public int Health;

    void Start()
    {
        Initialize();
        AddToMap();
        
    
    }

    void Update()
    {
        CheckStatus();
    }

    public virtual void Initialize()
    {
        SelectorArrow = GameObject.Find("SelectorArrow"); //object that will be used to highlight towers 
        Board = GameObject.Find("PlayBoard").GetComponent<BoardController>(); //the game board script
    }

    public virtual void AddToMap()
    {
        //adding this tower to map array so it will be known about
        Vector2 CurPos = new Vector2(this.transform.position.x, this.transform.position.z);

        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].walkable = false;
        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].Tower = this.gameObject;

        EnemiesInRange = new List<GameObject>();
    }

    public virtual void SetHealth(int _health)
    {
        Health = _health;
    }

    public virtual void CheckStatus()
    {
         if(Health <= 0)
         {
             Destroy(this.gameObject);
         }
    }

    public virtual void Highlight()
    {
        
        //when selected yellow square will be plased under tower
        SelectorArrow.transform.position = this.gameObject.transform.position + new Vector3 (0,-0.5f,0);
        //SelectorArrow.transform.localScale = new Vector3(0.8f, 0.05f, 0.8f);

        SelectorArrow.gameObject.transform.parent = this.gameObject.transform;

        SelectorArrow.transform.localScale = new Vector3(0.8f, 0.05f, 0.8f);
        
    }


    public virtual void MoveTo(Vector2 coords) // TODO - add colldown
    {
        
        Vector2 CurPos = new Vector2(this.transform.position.x, this.transform.position.z);

        //Removing from map
        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].walkable = true;
        Board.ActualMap[(int)CurPos.x, (int)CurPos.y].Tower = null;
        
        //moving
        this.gameObject.transform.position = new Vector3(coords.x, 1.5f, coords.y);

        //adding to the map
        Board.ActualMap[(int)coords.x, (int)coords.y].walkable = false;
        Board.ActualMap[(int)coords.x, (int)coords.y].Tower = this.gameObject;

        //Debug.Log("Mov: " + CurPos.x + "/" + CurPos.y + " to: " + coords.x + "/" + coords.y);
    }


    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag.Equals("Enemy"))
         {
             EnemiesInRange.Add(other.gameObject);
         }

    }


    void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag.Equals("Enemy"))
         {
             EnemiesInRange.Remove(other.gameObject);
         }

    }
    
   
   
}
