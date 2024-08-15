using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum states
{
    Disabled,
    Roaming,
    Chasing,
    Attacking
}

public class EnemyAI : MonoBehaviour
{
    public static EnemyAI instance;

    public NavMeshAgent agent;
    [Range(1, 50)] public float walkRadius;
    public states state;

    private void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (state)
        {
            case states.Disabled:
                break;
            case states.Roaming:
                Roaming();
                break;
            case states.Chasing:
                break;
        }
    }

    private void Roaming()
    {
        if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshPosition());
        }
    }

    public void Chase(Transform pos)
    {
        if (state != states.Disabled)
        {
            state = states.Chasing;
            agent.SetDestination(pos.position);
            if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
            {
                state = states.Roaming;
                print("s");
            }
        }
    }

    private Vector3 RandomNavMeshPosition()
    {
        Vector3 finalPos = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * walkRadius;
        randomPos += transform.position;
        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, walkRadius, 1))
        {
            finalPos = hit.position;
        }
        return finalPos;
    }
}
