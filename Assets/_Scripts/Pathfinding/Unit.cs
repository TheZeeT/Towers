using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{


    //public Transform target;
    Vector3[] path;
    int targetIndex;

    public bool WAIT = false;

    public float speed = 1;


    public void MoveToTargetPos(Vector3 TargetPos)
    {
        GameObject.Find("PlayBoard").GetComponent<PathRequestManager>().RequestPath(transform.position, TargetPos, gameObject);
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

            while (WAIT)
            {
                yield return null;
            }

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
            if(currentWaypoint != transform.position)
            {
                Vector3 relativePos = currentWaypoint - transform.position;
                transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            }
            
            
            
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