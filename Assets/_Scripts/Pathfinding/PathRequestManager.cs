using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{
    public struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;

        public GameObject PathingEntity;

        public PathRequest(Vector3 _start, Vector3 _end, GameObject _Entity)
        {
            pathStart = _start;
            pathEnd = _end;
            PathingEntity = _Entity;

        }

    }


    public Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();

    PathRequest currentPathRequest;

    bool isProcessingPath;

    public Pathfinding PathToPoint;

    void Awake()
    {

    }

    public void RequestPath(Vector3 pathStart, Vector3 pathEnd, GameObject Entity)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, Entity);
        pathRequestQueue.Enqueue(newRequest);
        TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            PathToPoint.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        //currentPathRequest.callbackV3 = path; 
        //currentPathRequest.callback = success;

        currentPathRequest.PathingEntity.GetComponent<Unit>().PathFound(path, success);

        isProcessingPath = false;
        TryProcessNext();
    }

}
