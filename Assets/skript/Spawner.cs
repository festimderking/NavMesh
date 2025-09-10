using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject endpoint;

    private List<NavMeshPath> paths = new List<NavMeshPath>();
    private  List<NavMeshPath> endpaths = new List<NavMeshPath>();

    float timerspawner = 0f;
    int waves = 3;
    int wavesize = 1;
    public GameObject enemyprefab;

    public GameObject team;
    void Start()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, waypoints[i].transform.position, NavMesh.AllAreas, path);
            paths.Add(path);

        }
        for (int i = 0; i < waypoints.Length; i++)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(waypoints[i].transform.position, endpoint.transform.position, NavMesh.AllAreas, path);
            endpaths.Add(path);

        }
        StartCoroutine(spawning());
    }

    void Update()
    {
        timerspawner -= Time.deltaTime;
        if (timerspawner > 0f)
        {
            StartCoroutine(spawning());
            timerspawner = 15f;
        }
    }

    IEnumerator spawning()
    {
        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < wavesize; j++)
            {

                GameObject enemy = Instantiate(enemyprefab, transform.position, Quaternion.identity, team.transform);
                enemy.name = $"Enemy_{i}_{j}";
                enemy.GetComponent<Enemy>().spawner = this;
                enemy.GetComponent<Enemy>().path = paths[i];

                yield return new WaitForSeconds(0.25f);
            }

            yield return new WaitForSeconds(1f);
        }
    }
    
    public NavMeshPath EndPoint(NavMeshPath path)
    {
        int index = paths.IndexOf(path);
        return endpaths[index];
        
    }
}
