using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{

    public Camera player_camera;
    public GameObject Selected_Tower;

    
    void Start()
    {
        
    }

    
    void Update()
    {

        //Mouse imputs

        if(Input.GetMouseButtonDown(0)) //LMB
        {
            RaycastHit hit;
            Ray ray = player_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) //raycasting on mouse click
            {


                if (hit.transform.tag == "Tower") //when clicking a tower
                {
                  
                    Selected_Tower = hit.transform.gameObject.transform.parent.gameObject;  //changing curent tower on click

                    Selected_Tower.GetComponent<TowerBase>().Highlight();
                }



                if (hit.transform.tag == "PlayBoard" && Selected_Tower != null) //when clicking a board
                {
                    //turning the vector3 of raycast to int to find a cell position
                    Vector2 cell;
                    cell.x = Mathf.Round(hit.point.x);
                    cell.y = Mathf.Round(hit.point.z);

                    //Using internal function to move the tower
                    Selected_Tower.GetComponent<TowerBase>().MoveTo(new Vector2(cell.x, cell.y));
                
                } 
            }
        }


        
    }


    public void PlaceCamera(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
    }


}
