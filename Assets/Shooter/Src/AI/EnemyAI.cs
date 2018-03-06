using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamageable
{
    private enum State
    {
        Chasing,
        Attacking,
        Dead
    }

    [SerializeField] private float closeEnoughDistance;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float damageInterval = 0.5f;

    private Animator _animator;
    private NavMeshAgent _navmeshAgent;

    private Vector3 targetPos;

    private State lastState;
    private State currentState;

    private bool stateMachineInitiated = false;
    private int health;
    private float lastHitTime;

    private void Start()
    {
        health = maxHealth;

        _animator = GetComponentInChildren<Animator>();
        _navmeshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            if (!stateMachineInitiated || lastState != currentState)
            {
                stateMachineInitiated = true;

                switch (currentState)
                {
                    case State.Chasing:
                        StartedChasing();
                        break;
                    case State.Attacking:
                        StartedAttacking();
                        break;
                    case State.Dead:
                        StartedDeath();
                        break;
                }

                lastState = currentState;
            }

            Ticker();
            yield return null;
        }

    }

    private void Ticker()
    {
        if (health <= 0)
        {
            Die();
            return;
        }

        targetPos = ShooterGameManager.Player.transform.position;
        UpdateNavigation();

        bool isCloseEnough = Vector3.Distance(transform.position, targetPos) <= closeEnoughDistance;

        switch (currentState)
        {
            case State.Attacking:
                if (!isCloseEnough) currentState = State.Chasing;

                if (Time.realtimeSinceStartup - lastHitTime > damageInterval)
                {
                    ShooterGameManager.Player.SendMessage("ApplyDamage", 15);
                    ResetHitTime();
                }

                break;
            case State.Chasing:
                if (isCloseEnough) currentState = State.Attacking;
                break;
        }
    }


    private void StartedDeath()
    {
        StopMovement();
        _animator.SetTrigger("Dead");
    }

    private void StartedChasing()
    {
        _navmeshAgent.isStopped = false;
        SetAttackingAnimation(false);
    }

    private void StartedAttacking()
    {
        StopMovement();
        SetAttackingAnimation(true);
        ResetHitTime();
    }

    private void ResetHitTime()
    {
        lastHitTime = Time.realtimeSinceStartup;
    }

    private void SetAttackingAnimation(bool flag)
    {
        _animator.SetBool("IsAttacking", flag);
    }

    private void UpdateNavigation()
    {
        _navmeshAgent.SetDestination(targetPos);
    }

    private void StopMovement()
    {
        _navmeshAgent.isStopped = true;
        _navmeshAgent.velocity = Vector3.zero;
    }

    public void ApplyDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
    }

    public void Die()
    {
        currentState = State.Dead;
        Destroy(gameObject, 3f);
    }
}
