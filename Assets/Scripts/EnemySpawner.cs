using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public bool useNavMeshPlacement = true;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = transform.position;

        if (useNavMeshPlacement)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPos, out hit, 1.0f, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
            }
            else
            {
                Debug.LogWarning("Spawn point not on NavMesh: " + gameObject.name);
                return;
            }
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
