using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject Board;


    public int BoardHalfSizeX;
    public int BoardHalfSizeZ;

    
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
        //Change Board Size
        Board.transform.localScale = new Vector3(BoardHalfSizeX * 2, 1, BoardHalfSizeZ * 2);

        //Change Boards Square Size
        Board.GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2(BoardHalfSizeX, BoardHalfSizeZ));


    }


}
