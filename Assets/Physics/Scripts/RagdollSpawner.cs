using System.Collections;
using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ragdollPrefab;

    [SerializeField]
    private float minSpawnTime = 0.1f;

    [SerializeField]
    private float maxSpawnTime = 1f;

    [SerializeField]
    private float spawnPositionOffset = 1.5f;

    [SerializeField]
    private bool allowToDestroyRagdolls = false;

    [SerializeField]
    private float ragdollInstanceDestroyTimeInSeconds = 2f;

    void Start()
    {
        StartCoroutine(SpawnRagdollsRoutine());
    }

    private IEnumerator SpawnRagdollsRoutine()
    {
        while (true)
        {
            SpawnRagdoll();

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            yield return new WaitForSeconds(spawnTime);
        }

    }
    private void SpawnRagdoll()
    {
        float xOffset = Random.Range(-spawnPositionOffset, spawnPositionOffset);
        float yOffset = Random.Range(-spawnPositionOffset, spawnPositionOffset);
        float zOffset = Random.Range(-spawnPositionOffset, spawnPositionOffset);

        Vector3 positionOffset = new Vector3(xOffset, yOffset, zOffset);

        GameObject spawnedRagdollInstance = Instantiate(ragdollPrefab, transform.position + positionOffset, Quaternion.identity);

        if (allowToDestroyRagdolls)
        {
            Destroy(spawnedRagdollInstance, ragdollInstanceDestroyTimeInSeconds);
        }
    }
}
