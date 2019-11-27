using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{

    public Camera player_camera;
    public GameObject Selected_Tower;


    public float cameraSpeedMultiplayer = 0.5f;
    public float zoomMultiplayer = 0.1f;

    public Vector3 temphit;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        //Mouse imputs
        CameraControl();

        UnitControl();

        

       
    }

    public void PlaceCamera(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
    }


    void CameraControl()
    {
        
            //Camera movement depending on looking direction
        
        float frwrd = Input.GetAxis("Horizontal");
        float sdwys = Input.GetAxis("Vertical");

        this.gameObject.transform.parent.transform.Translate(frwrd * cameraSpeedMultiplayer, 0f, sdwys * cameraSpeedMultiplayer, Space.Self);


            //Rotating camera around using RMB

        if (Input.GetMouseButton(1))
        {
            this.gameObject.transform.parent.transform.RotateAround(this.gameObject.transform.position, Vector3.up, (Input.GetAxis("Mouse X") * 100) * Time.deltaTime);

        }

            //Moving camera up and down using scroll wheel
            //Creating vector3 that determains line of movement for zooming
            //TODO: Envestigate zooming towards cursor position

        Vector3 LookDir = new Vector3();

        LookDir.z = gameObject.transform.eulerAngles.x + 10f;
        LookDir.y = 10f - gameObject.transform.eulerAngles.x;

        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            this.gameObject.transform.parent.transform.Translate(LookDir * Input.GetAxis("Mouse ScrollWheel") * zoomMultiplayer);
        }

    }


    


    void UnitControl()
    {
        if (Input.GetMouseButtonDown(0)) //LMB
        {

            LayerMask Clickable = LayerMask.GetMask("Units", "Enemies", "BG");



            RaycastHit hit;
            Ray ray = player_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, Clickable)) //raycasting on mouse click
            {
                temphit = hit.point;

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

    public void OnDrawGizmos()
    {
        if(temphit != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(temphit, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
}
