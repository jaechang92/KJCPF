using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCTween : MonoBehaviour
{
    public NavMeshAgent agent;
    public iTweenPath path;
    public List<Vector3> pathList;
    public float agentSpeed;


    private Vector3 tmpVector = Vector3.zero;

    void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        path = GetComponent<iTweenPath>();

        AddPathPointList();
    }

    void Start()
    {
        Init();

        
    }

    void Update()
    {
        MoveToAgent();
        //agent.destination = Vector3.zero;
    }

    public void AddPathPointList()
    {
        foreach (var item in path.nodes)
        {
            tmpVector.x = item.x;
            tmpVector.z = item.z;

            pathList.Add(tmpVector);
        }

    }


    public int pathIndex = 0;
    private bool goBack = false;
    public void MoveToAgent()
    {
        agent.destination = pathList[pathIndex];

        if (agent.transform.position.x == pathList[pathIndex].x && agent.transform.position.z == pathList[pathIndex].z)
        {
            if (pathIndex == pathList.Count - 1)
            {
                goBack = true;
            }
            else if (pathIndex == 0)
            {
                goBack = false;
            }

            if (goBack)
            {
                pathIndex--;
            }
            else
            {
                pathIndex++;
            }
        }
        
        
    }
}
