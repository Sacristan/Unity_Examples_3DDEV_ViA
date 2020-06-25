using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RigidbodySimulationController : MonoBehaviour
{

    [SerializeField] private float wanderRange = 10f;
    [SerializeField] private float closeEnoughDistance = 0.5f;
    [SerializeField] private float minIdleTime = 2f;
    [SerializeField] private float maxIdleTime = 5f;
    [SerializeField] Transform puppetLegL;
    [SerializeField] Transform puppetLegR;

    [SerializeField] Transform puppetFootR;
    [SerializeField] Transform puppetFootL;

    [SerializeField] float legAnimationAmount = 1f;
    [SerializeField] float legAnimationSpeed = 1f;

    [SerializeField] float footAnimationAmount = 1f;

    [SerializeField] float footAnimationSpeed = 1f;

    NavMeshAgent _navmeshAgent;

    private Vector3 currentTarget;
    private bool isCloseEnough;

    void Start()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(WanderRoutine());
        StartCoroutine(Animate());
    }

    IEnumerator WanderRoutine()
    {
        CalcWanderPos();

        while (true)
        {
            isCloseEnough = Vector3.Distance(transform.position, currentTarget) <= closeEnoughDistance;

            if (isCloseEnough)
            {
                yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
                CalcWanderPos();
            }

            yield return null;
        }
    }



    void CalcWanderPos()
    {
        if (RandomPoint(transform.position, wanderRange, out Vector3 wanderPos))
        {
            _navmeshAgent.SetDestination(wanderPos);
            currentTarget = wanderPos;
        }
    }

    IEnumerator Animate()
    {
        float t = 0f;

        while (true)
        {
            yield return null;

            if (isCloseEnough)
            {
                t = 0f;

                puppetFootR.transform.localPosition = Vector3.zero;
                puppetFootL.transform.localPosition = Vector3.zero;
                puppetLegL.transform.localPosition = Vector3.zero;
                puppetLegR.transform.localPosition = Vector3.zero;
            }
            else
            {
                t += Time.deltaTime;

                puppetFootR.transform.localPosition = Vector3.forward * footAnimationAmount * Mathf.Cos(t * footAnimationSpeed);
                puppetFootL.transform.localPosition = Vector3.forward * footAnimationAmount * Mathf.Sin(t * footAnimationSpeed);

                puppetLegL.transform.localPosition = Vector3.forward * legAnimationAmount * Mathf.Sin(t * legAnimationSpeed);
                puppetLegR.transform.localPosition = Vector3.forward * legAnimationAmount * Mathf.Cos(t * legAnimationSpeed);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }


}
