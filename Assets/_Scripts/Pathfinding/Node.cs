using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    //position
    public int x;
    public int y;

    public int gCost; // distance from starting node
    public int hCost; // distance to target node

    public int fcost; // fcost = gCost + hCost

    public Node parent;

    public Node(int _x, int _y, int _gCost, int _hCost)
    {

        x = _x;
        y = _y;

        gCost = _gCost;
        hCost = _hCost;

        fcost = gCost + hCost;
    }

    public Node(int _x, int _y)
    {

        x = _x;
        y = _y;

        gCost = 0;
        hCost = 0;

        fcost = gCost + hCost;
    }

}
