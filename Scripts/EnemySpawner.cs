using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 15f;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1, spawnInterval);//spawn setelah 1 detik 
    }

    void SpawnEnemy()
    {
        Vector3 pos = Random.insideUnitSphere * spawnRadius;
        pos.y = 0;
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}

