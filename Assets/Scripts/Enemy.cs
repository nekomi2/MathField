using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange;
    public bool playerInSightRange;
    public float stopDistance = 5.0f;

    private Animator anim;

    private void Awake()
    {
        player = GameObject.Find("MaleCharacter").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (!playerInSightRange) Patroling();
        if (playerInSightRange) ChasePlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            agent.SetDestination(player.position);
            anim.SetBool("isRunningForward", true);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackward", false);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            agent.ResetPath();
            anim.SetBool("isRunningForward", false);
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackward", false);
            anim.SetBool("isAttacking", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MaleCharacter")
        {
            other.gameObject.GetComponent<Animator>().SetBool("isDefending", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MaleCharacter")
        {
            other.gameObject.GetComponent<Animator>().SetBool("isDefending", false);
        }
    }


}
