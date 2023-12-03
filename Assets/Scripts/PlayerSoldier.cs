using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoldier : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    // public Transform player;
    public PlayerArmy playerArmy;

    private void Awake()
    {
        // Get the army which is the parent
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerArmy.transform.position);
    }

}
