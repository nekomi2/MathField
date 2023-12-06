using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    // public Transform enemy;
    public EnemyArmy enemyArmy;

    private void Awake()
    {
        // Get the army which is the parent
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        ChaseEnemy();
    }

    private void ChaseEnemy()
    {
        agent.SetDestination(enemyArmy.transform.position);
    }

}
