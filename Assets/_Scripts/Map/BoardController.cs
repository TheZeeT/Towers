using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public struct Map
    {
        public float WorldPosX;
        public float WorldPosY;

        public int GridPosX;
        public int GridPosY;

        public GameObject Tower;
        
        public bool walkable;
    }


    public Map[,] ActualMap; //The ACTUAL map of the ACTUAL level

    public GameObject Board;

    //public static Vector3 globalOffset = new Vector3(0.5f, 0.5f, 0.5f);
    public static Vector3 globalOffset = new Vector3(0.0f, 0.0f, 0.0f); //Temp for testing

    public int BoardHalfSizeX;
    public int BoardHalfSizeZ;


    public Camera MainCamera;


    void Start()
    {
        BoardHalfSizeX = 5;
        BoardHalfSizeZ = 5;

        ActualMap = new Map[BoardHalfSizeX * 2, BoardHalfSizeZ * 2];

        SetMapSize();
        TempFillMap();
    }

    
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            SetMapSize();
        }
    }

    public void SetMapSize()
    {
        //place board so corner tile be at x = 0, z = 0
        Board.transform.position = new Vector3(BoardHalfSizeX - 0.5f, 1.0f, BoardHalfSizeZ - 0.5f);

        //Change Board Size
        Board.transform.localScale = new Vector3(BoardHalfSizeX * 2, 1, BoardHalfSizeZ * 2);

        //Change Boards Squares Size
        Board.GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2(BoardHalfSizeX, BoardHalfSizeZ));

        //center Camera towards the board 
        MainCamera.GetComponent<PlayerCameraScript>().PlaceCamera(new Vector3(BoardHalfSizeX, 8.0f, BoardHalfSizeZ - 10.0f));

    }

    void TempFillMap()
    {
        
        //temp map fill for dev 
        //delete later

        for(int i = 0; i < BoardHalfSizeX * 2; i++ )
        {
            for (int j = 0; j < BoardHalfSizeZ * 2; j++)
            {
                ActualMap[i, j].GridPosX = i;
                ActualMap[i, j].GridPosY = j;

                ActualMap[i, j].WorldPosX = i;
                ActualMap[i, j].WorldPosY = j;

                ActualMap[i, j].walkable = true;

                ActualMap[i, j].Tower = null;
            }
        }

        if (ActualMap[1, 1].walkable == true)
        {
            Debug.Log("WTF");
        }

    }

    public void OnDrawGizmos()
    {
        for (int i = 0; i < BoardHalfSizeX * 2; i++)
        {
            for (int j = 0; j < BoardHalfSizeZ * 2; j++)
            {
                if (ActualMap != null)
                {
                    if (ActualMap[1, 1].walkable == false)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(i, 2.0f, j), new Vector3(0.1f, 0.1f, 0.1f));
                    }
                    else
                    {
                        Gizmos.color = Color.black;
                    }
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                
               
               

               
               

            }
        }


    }


}
