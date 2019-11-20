using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{


    public Transform target;
    float speed = 5;
    Vector3[] path;
    int targetIndex;

    //void Start()
    //{
        
    //}

    //void Update()
    //{
    //    if(Input.GetKeyDown("z"))
    //    {
    //        GameObject.Find("PathingLord").GetComponent<PathRequestManager>().RequestPath(transform.position, target.position, gameObject);
    //        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    //    }
    //}

    public void MoveToTargetPos(Vector3 TargetPos)
    {
        GameObject.Find("PathingLord").GetComponent<PathRequestManager>().RequestPath(transform.position, TargetPos, gameObject);
    }

    public void PathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;

            //Debug.Log("lenght = " + path.Length);
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            Debug.Log("Pathfinding Fail");
        }
    }

    IEnumerator FollowPath()
    {
        if(path.Length == 0)
        {
            Debug.Log("You already there!");
            yield break;
        }
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i] + BoardController.globalOffset, Vector3.one * 0.4f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position + BoardController.globalOffset, path[i] + BoardController.globalOffset);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1] + BoardController.globalOffset, path[i] + BoardController.globalOffset);
                }
            }
        }
    }
}