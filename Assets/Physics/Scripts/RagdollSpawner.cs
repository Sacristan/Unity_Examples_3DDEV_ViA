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
            StartCoroutine(KillRoutine(spawnedRagdollInstance));
        }
    }


    private IEnumerator KillRoutine(GameObject killObject){
        yield return new WaitForSeconds(ragdollInstanceDestroyTimeInSeconds);

        Collider[] ragdollColliders = killObject.GetComponentsInChildren<Collider>();

        for(int i = 0; i < ragdollColliders.Length; i++)
        {
            Collider collider = ragdollColliders[i];
            collider.enabled = false;
        }

        // yield return new WaitForSeconds(1);
        // Destroy(killObject);
        Destroy(killObject, 1f);

    }

}
