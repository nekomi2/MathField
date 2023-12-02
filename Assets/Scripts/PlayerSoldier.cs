using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoldier : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    // public Transform player;
    public Transform playerArmy;

    private void Awake()
    {
        playerArmy = GameObject.Find("Player Army").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerArmy.position);
    }
}
