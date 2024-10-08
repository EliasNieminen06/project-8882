using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [Range(0, 5)] public float roamSpeed;
    [Range(0, 5)] public float chaseSpeed;
    [Range(0, 1)] public float destinationTreshold;
    public states state;

    private GameObject player;

    private void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
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
                Chasing();
                break;
            case states.Attacking:
                break;
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            Vector3 playerstartpos = player.transform.position;
            if (player.transform.position != playerstartpos)
            {
                if (state != states.Attacking)
                {
                    state = states.Attacking;
                    Attack(player.transform.position);
                }
            }
        }
    }

    private void Roaming()
    {
        agent.speed = roamSpeed;
        if (agent != null && agent.remainingDistance <= agent.stoppingDistance + destinationTreshold)
        {
            agent.SetDestination(RandomNavMeshPosition());
        }
    }

    private void Chasing()
    {
        agent.speed = chaseSpeed;
        if (agent != null && agent.remainingDistance <= agent.stoppingDistance + destinationTreshold)
        {
            state = states.Roaming;
        }
    }

    private void Attack(Vector3 pos)
    {
        agent.SetDestination(pos);
        Debug.Log("attacked");
    }

    public void Chase(Vector3 pos)
    {
        if (state != states.Disabled)
        {
            if (NavMesh.SamplePosition(pos, out NavMeshHit hit, walkRadius, 1))
            {
                agent.SetDestination(hit.position);
                state = states.Chasing;
            }
            else
            {
                Debug.LogWarning("Impossible destination");
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
