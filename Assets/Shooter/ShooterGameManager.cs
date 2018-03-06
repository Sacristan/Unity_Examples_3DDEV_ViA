using UnityEngine;
using UnityEngine.AI;

public class ShooterGameManager : MonoBehaviour
{
    [System.Serializable]
    private class EnemySpawner
    {
        private const float SpawnArea = 50f;
        private const int SpawnTries = 30;

        [SerializeField]
        private EnemyAI enemySpawnPrefab;

        private int difficulty = 0;

        public EnemyAI EnemyAIPrefab { get { return enemySpawnPrefab; } }

        public void IncreaseDifficulty()
        {
            difficulty++;
        }

        public void Spawn(Vector3 center)
        {
            int spawnableEnemies = 1 + difficulty;

            for (int i = 0; i < spawnableEnemies; i++)
            {
                Vector3 spawnPos = GetSpawnPos(center);
                Instantiate(enemySpawnPrefab, spawnPos, Quaternion.identity);
            }
        }

        private Vector3 GetSpawnPos(Vector3 center)
        {
            for (int i = 0; i < SpawnTries; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * SpawnArea;

                NavMeshHit hit;

                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return Vector3.zero;
        }

    }

    private static ShooterGameManager instance;

    private GameObject _player;

    [SerializeField] private EnemySpawner[] enemySpawners;

    public static GameObject Player
    {
        get
        {
            if (instance._player == null)
            {
                instance._player = GameObject.FindGameObjectWithTag("Player");
            }

            return instance._player;
        }

    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].Spawn(transform.position);
        }
    }

    public static void EnemyDied(EnemyAI enemyDied)
    {
        for (int i = 0; i < instance.enemySpawners.Length; i++)
        {
            EnemySpawner enemySpawner = instance.enemySpawners[i];

            if (enemySpawner.EnemyAIPrefab.GetType() == enemyDied.GetType())
            {
                enemySpawner.IncreaseDifficulty();
                enemySpawner.Spawn(instance.transform.position);
            }

        }
    }

    public static void ApplyDamageToPlayer(int damage)
    {
        IDamageable damageAbleInterface = Player.GetComponent<IDamageable>();
        if (damageAbleInterface != null) damageAbleInterface.ApplyDamage(damage);
    }

}
