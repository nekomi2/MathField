using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}
