using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public NavMeshPath path;

    public Spawner spawner;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bool pathset = agent.SetPath(path);
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5 && path != null)
        {
            agent.SetPath(spawner.EndPoint(path));

        }
    }
}
