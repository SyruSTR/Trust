using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeletons : MonoBehaviour
{
    private Transform[] spawnpoints = new Transform[9];
    public Transform[] selectPoints = new Transform[6];
    public List<Transform> spawnPoint;
    public GameObject skeleton;
    public int minSkeletonSpawn = 1;
    public int maxSkeletonSpawn = 3;
    [Space]
    public int minSpawnpoints = 2;
    public int maxSpawnpoints = 6;
    public float enemySpawnTime = 10f;
    public float timeReductionSpawn = 0.5f;
    void Start()
    {
        int iCount = 0;
        foreach (GameObject spawnpoint in GameObject.FindGameObjectsWithTag("Spawnpoints"))
        {
            spawnpoints[iCount] = spawnpoint.transform;
            iCount++;
        }
        SelectPoint();
        StartCoroutine(SpawnEnemy(enemySpawnTime));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            SelectPoint();

    }
    void SelectPoint()
    {
        for (int i = 0; i < selectPoints.Length; i++)
        {
            selectPoints[i] = null;
            spawnPoint.Clear();
        }
        int CountSpawnpoints = Random.Range(minSpawnpoints, maxSpawnpoints);
        for (int i = 0; i < CountSpawnpoints; i++)
        {
            spawnPoint.Add(spawnpoints[Random.Range(0, spawnpoints.Length - 1)]);
        }
        for (int i = 0; i < spawnPoint.Count; i++)
        {
            for (int j = 0; j < spawnPoint.Count; j++)
            {
                if(spawnPoint[i] == spawnPoint[j] && i!=j)
                {
                    spawnPoint.RemoveAt(i);
                }
            }
        }
        for (int i = 0; i < spawnPoint.Count; i++)
        {
            selectPoints[i] = spawnPoint[i];
        }

    }
    IEnumerator SpawnEnemy(float t)
    {
        foreach (Transform point in spawnPoint)
        {            
                Instantiate(skeleton, point.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0),Quaternion.identity);
            
        }
        SelectPoint();
        enemySpawnTime -= timeReductionSpawn;
        
        yield return new WaitForSeconds(t);
        Debug.Log("Good\n" + enemySpawnTime);
        if (enemySpawnTime > timeReductionSpawn)
        {
            StartCoroutine(SpawnEnemy(enemySpawnTime));
            Debug.Log("111");
        }
    }
}