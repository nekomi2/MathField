using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSlime : MonoBehaviour
{
    public Powerup powerup;
    public float detectionRadius = 50f;
    public float innerRadius = 10f;
    public float outerRadius = 50f;
    public float speed = 10f;
    private Transform target;
    private NavMeshAgent agent;
    private Vector3 startingPosition;

    private float time = 0.0f;

    private enum State { Circling, Chasing };
    private State currentState = State.Circling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        startingPosition = transform.position;
    }

    void Update()
    {
        if (powerup == null)
        {
            Destroy(gameObject);
        }
        time += Time.deltaTime;
        if (time > 1f)
        {
            time = 0.0f;
            ProtectPowerup();
        }
    }

    void ProtectPowerup()
    {
        DetectTargets();
        if (currentState == State.Chasing)
        {
            ChaseTarget();
        }
        else if (currentState == State.Circling)
        {
            CirclePowerup();
        }
    }
    void CirclePowerup()
    {
        float randomRadius = Random.Range(innerRadius, outerRadius);
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        Vector3 offset = new Vector3(Mathf.Sin(randomAngle), 0, Mathf.Cos(randomAngle)) * randomRadius;
        agent.SetDestination(powerup.transform.position + offset);
    }



    void DetectTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(powerup.transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Debug.Log("Target detected");
                target = hitCollider.transform;
                currentState = State.Chasing;
                break;
            }
        }

        if (target != null && Vector3.Distance(powerup.transform.position, target.position) > detectionRadius)
        {
            // Debug.Log("Stopped Chasing");
            target = null;
            currentState = State.Circling;
        }
    }

    void ChaseTarget()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }

}
