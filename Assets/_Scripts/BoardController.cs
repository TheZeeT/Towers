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

        public int block_id;
        
        public bool walkable;
    }


    public Map[,] ActualMap; //The ACTUAL map of the ACTUAL level

    public GameObject Board;

    public static Vector3 globalOffset = new Vector3(0.5f, 0.5f, 0.5f); 

    public int BoardHalfSizeX;
    public int BoardHalfSizeZ;


    public Camera MainCamera;


    void Start()
    {
        BoardHalfSizeX = 5;
        BoardHalfSizeZ = 5;

        SetMapSize();
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
        //place board so corner be an x = 0, z = 0
        Board.transform.position = new Vector3(BoardHalfSizeX + 0.5f, 1.0f, BoardHalfSizeZ + 0.5f);

        //Change Board Size
        Board.transform.localScale = new Vector3(BoardHalfSizeX * 2, 1, BoardHalfSizeZ * 2);

        //Change Boards Square Size
        Board.GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2(BoardHalfSizeX, BoardHalfSizeZ));


        MainCamera.GetComponent<PlayerCameraScript>().PlaceCamera(new Vector3(BoardHalfSizeX, 8.0f, BoardHalfSizeZ - 10.0f));

    }


}
