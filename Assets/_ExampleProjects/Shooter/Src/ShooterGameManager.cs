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

    public delegate void EventHandler();
    public delegate void PlayerDamageEventHandler(int health);
    public static event EventHandler OnPlayerDied;
    public static event PlayerDamageEventHandler OnPlayerReceivedDamage;

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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        OnPlayerDied += ShooterGameManager_OnPlayerDied;

        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].Spawn(transform.position);
        }
    }

    void ShooterGameManager_OnPlayerDied()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        FirstPersonController controller = _player.GetComponent<FirstPersonController>();
        Shooter shooter = _player.GetComponent<Shooter>();

        if (controller != null) controller.enabled = false;
        if (shooter != null) shooter.enabled = false;
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

    public static void PlayerReceivedDamage(int currentHealth)
    {

        if (OnPlayerReceivedDamage != null)
            OnPlayerReceivedDamage.Invoke(currentHealth);
    }

    public static void PlayerDied()
    {
        if (OnPlayerDied != null)
            OnPlayerDied.Invoke();
    }

}
