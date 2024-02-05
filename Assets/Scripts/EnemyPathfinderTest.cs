using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding;

public class EnemyPathfinderTest : MonoBehaviour
{
    Pathfinder<Vector3> pathfinder;
    List<Vector3> path = new List<Vector3>();

    private void Start()
    {
        pathfinder = new Pathfinder<Vector3>(GetDistance, GetNeightbourNodes);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (pathfinder.GenerateAstarPath(transform.position, target, out path))
            {
                transform.position = path[0];
                path.RemoveAt(0);
            }
        }
    }

    private float GetDistance(Vector3 A, Vector3 B)
    {
        return (A - B).sqrMagnitude;
    }

    private Dictionary<Vector3, float> GetNeightbourNodes(Vector3 pos)
    {
        Dictionary<Vector3, float> neighbours = new Dictionary<Vector3, float>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    if (i == 0 && j == 0 && k == 0) continue;
                    Vector3 dir = new Vector3(i, j, k);
                    if (!Physics.Linecast(pos, pos + dir))
                    {
                        neighbours.Add(pos + dir, dir.magnitude);
                    }
                }
            }
        }
        return neighbours;
    }
}
