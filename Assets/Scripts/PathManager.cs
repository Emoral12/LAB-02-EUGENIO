using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PathManager : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    public List<Waypoint> path;

    public GameObject prefab; 
    int currentPointIndex = 0;
    public List<GameObject> prefabPoints;

    public void Start()
    {
        prefabPoints = new List<GameObject>();
        // create prefab colliders for the path locations
        foreach(Waypoint p in path)
        {
            GameObject go = Instantiate(prefab); 
            go.transform.position= p.pos; 
            prefabPoints.Add(go);
        }
    }
    public void Update()
    {
        // update all of the prefabs to the waypoint locations
        for(int i = 0; i < path.Count; i++)
        {
            Waypoint p = path[i];
            GameObject g = prefabPoints[i]; 
            g.transform.position = p.pos;
        }
    }

    public List<Waypoint> GetPath()
    {
        if (path == null)
            path = new List<Waypoint>();

        return path;
    }

    public void CreateAddPoint()
    {
        Waypoint go = new Waypoint(); 
        path.Add(go);
    }

    public Waypoint GetNextTarget()
    {
        int nextPointIndex = (currentPointIndex + 1)% (path.Count);
        currentPointIndex = nextPointIndex;
        return path[nextPointIndex];
    }
}