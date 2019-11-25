using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Pathfinding : MonoBehaviour
{

    public BoardController.Map[,] MAP;

    List<Node> openSet = new List<Node>();
    List<Node> closedSet = new List<Node>();

    public int Iterations = 0;

    public void AwakeOnCommand()
    {
        MAP = GameObject.Find("PlayBoard").GetComponent<BoardController>().ActualMap;
    }

    void Update()
    {
        if(Input.GetKeyDown("h"))
        {
            Debug.Log(GetDistance(0, 0, 10, 10));
        }
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector2 startCell = new Vector2(startPos.x,startPos.z);
        Vector2 targetCell = new Vector2(targetPos.x,targetPos.z);

        StartCoroutine(FindPath(startCell, targetCell));
    }


    IEnumerator FindPath(Vector2 startPos, Vector2 targetPos)
    {
        
        AwakeOnCommand();
        openSet.Clear();
        closedSet.Clear();

        Iterations = 0;

        Vector3[] waypoints = new Vector3[0];

        bool pathSuccess = false;

        int StX = Mathf.RoundToInt(startPos.x);
        int StY = Mathf.RoundToInt(startPos.y);

        int TgX = Mathf.RoundToInt(targetPos.x);
        int TgY = Mathf.RoundToInt(targetPos.y);

        Node startNode = new Node(StX, StY, 0, GetDistance(StX, StY, TgX, TgY));
        Node targetNode = new Node(TgX, TgY,  GetDistance(StX, StY, TgX, TgY), 0);

       

        if (MAP[StX, StY].walkable == true && MAP[TgX, TgY].walkable == true)
        {
            

            openSet.Add(startNode);

            while(openSet.Count > 0)
            {
                Iterations++;

                openSet.Sort((p1, p2) => p1.fcost.CompareTo(p2.fcost));

                Node currentNode = openSet[0];
                openSet.RemoveAt(0);
                closedSet.Add(currentNode);

                if(currentNode.x == Mathf.RoundToInt(targetPos.x) && currentNode.y == Mathf.RoundToInt(targetPos.y)) 
                {
                    targetNode.parent = currentNode.parent;
                    pathSuccess = true;
                    break;
                }

                for(int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if(i == 0 && j == 0)
                        {
                            continue;
                        }

                        //Whithin bounds
                        if (IsOutOfBounds(currentNode.x + i, currentNode.y + j))
                        {
                            continue;
                        }

                        //prevent from cliping corners when walk diagonaly
                        if ((Mathf.Abs(i) + Mathf.Abs(j)) > 1)
                        {
                            if (MAP[currentNode.x + i, currentNode.y].walkable == false || MAP[currentNode.x, currentNode.y + j].walkable == false)
                            {
                                continue;
                            }
                        }
                        

                        if (MAP[currentNode.x + i, currentNode.y + j].walkable == false || closedSet.Any(any => any.x == currentNode.x + i && any.y == currentNode.y + j)) 
                        {
                            continue;
                        }

                        Node neighbour = new Node(currentNode.x + i, currentNode.y + j);
                        neighbour.parent = currentNode;
                            

                        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode.x, currentNode.y, neighbour.x, neighbour.y);

                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Any(any => any.x == neighbour.x && any.y == neighbour.y))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour.x, neighbour.y, TgX, TgY);
                            neighbour.fcost = neighbour.gCost + neighbour.hCost;

                            neighbour.parent = currentNode;
                                

                            if (!openSet.Any(a => a.x == neighbour.x && a.y == neighbour.y))
                            {
                            openSet.Add(neighbour);
                            openSet.Sort((p1, p2) => p1.fcost.CompareTo(p2.fcost));
                            }
                          
                        }
                    }
                }
                


            }
        }

       

        yield return null;


        //Debug.Log("iterations = " + Iterations);

        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);

            if(waypoints.Length == 0)
            {
                pathSuccess = false;
            }
        }
        //Debug.Log("lengh = " + waypoints.Length + " succ = " + pathSuccess);
        gameObject.GetComponent<PathRequestManager>().FinishedProcessingPath(waypoints, pathSuccess);
        //requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    int GetDistance(int StX, int StY, int TgX, int TgY)
    {
        //Gets the distance 
        int dstX = Mathf.Abs(StX - TgX);
		int dstY = Mathf.Abs(StY - TgY);
		
		if (dstX > dstY) // Out of two lenghs (along X and Y) chose with is shorter and use it as a diagonal part, 
        {
            return 14*dstY + 10* (dstX-dstY);
        }
		else
        {
            return 14*dstX + 10 * (dstY-dstX);
        }
		

    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        //Debug.Log("endNode pos = " + endNode.x + endNode.y + endNode.z);
        //Debug.Log(" neods " + startNode.x + startNode.y + startNode.z + " and " + endNode.x + endNode.y + endNode.z);


        while (currentNode.x != startNode.x || currentNode.y != startNode.y)
        {
            //Debug.Log("curr node = " + currentNode.x + currentNode.y + currentNode.z );
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);

        Array.Reverse(waypoints);

        //Debug.Log(" count in path " + path.Count() + " count of waypoints = " + waypoints.Length);
        return waypoints;

    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector3 directionOld = Vector3.zero;

        if (path.Count == 0)
        {
            Debug.Log("Already there!");
            return waypoints.ToArray();
        }

        waypoints.Add(new Vector3(path[0].x, 1.5f, path[0].y));

        for (int i = 1; i < path.Count; i++)
        {
            Vector3 directionNew = new Vector3(path[i - 1].x - path[i].x, 1.5f, path[i - 1].y - path[i].y);
            if (directionNew != directionOld)
            {
                waypoints.Add(new Vector3(path[i].x, 1.5f, path[i].y));
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    bool IsOutOfBounds(int x, int y)
    {

        if(
            x < 0 ||
            y < 0 ||
            x > GameObject.Find("PlayBoard").GetComponent<BoardController>().BoardHalfSizeX * 2 ||
            y > GameObject.Find("PlayBoard").GetComponent<BoardController>().BoardHalfSizeZ * 2
            )
        {
            return true;
        }

        return false;
    }


}

    

