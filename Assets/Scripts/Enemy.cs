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
    public float stopDistance = 2.0f;
    private Animator anim;
    public int armySize = 1; // New variable for army size
    public EnemyArmy enemyArmy; // Reference to PlayerArmy
    public MainPlayer playerCharacter;
    private float combatTime = 0.0f;
    private bool inCombat = false;

    public float combatInterval = 0.2f;
    public bool isDead = false;

    private void Awake()
    {
        player = GameObject.Find("PlayerCharacter").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        if (playerCharacter.isDead)
        {
            anim.SetBool("isAttacking", false);
        }

        if (isDead || playerCharacter.isDead)
        {
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isAttacking", false);
            if(!isDead){
                anim.SetBool("isWinning", true);
            }
            agent.velocity = Vector3.zero;
            return;
        }
        if (inCombat)
        {
            if (Time.time - combatTime > combatInterval)
            {
                reduceArmySize();
                combatTime = Time.time;
            }
            if (armySize <= 0)
            {
                anim.SetBool("isDead", true);
                inCombat = false;
                isDead = true;
            }
        }
        if (armySize <= playerCharacter.armySize)
        {
            if (powerUps.Length > 0)
            {
                collectPowerUps(powerUps);
            }
            else
            {
                Patroling();
            }
        }

        else if (playerInSightRange && (playerCharacter.armySize < armySize || powerUps.Length == 0))
        {
            ChasePlayer();
        }

        // Check if the agent is moving
        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("isWalkingForward", true);
        }
        else
        {
            anim.SetBool("isWalkingForward", false);
        }
    }


    private void Patroling()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        if (powerUps.Length == 0)
        {
            walkPointSet = false;
        }

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }


    private void collectPowerUps(GameObject[] powerUps)
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        // Find the closest powerUp
        GameObject closestPowerUp = powerUps[0];
        float minDistance = Vector3.Distance(transform.position, closestPowerUp.transform.position);
        foreach (GameObject powerUp in powerUps)
        {
            float distance = Vector3.Distance(transform.position, powerUp.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPowerUp = powerUp;
            }
        }

        // Check if the closest powerUp still exists
        if (closestPowerUp != null)
        {
            // Set the walkPoint to the closest powerUp
            walkPoint = closestPowerUp.transform.position;
            walkPointSet = true;
        }
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
        }
        else
        {
            agent.ResetPath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "PlayerCharacter")
        {
            anim.SetBool("isAttacking", true);
            other.gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
            inCombat = true;
            combatTime = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "PlayerCharacter")
        {
            anim.SetBool("isAttacking", false);
            other.gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
            inCombat = false;
        }
    }

    public void increaseArmySize(int newArmySize)
    {
        if (newArmySize - armySize <= 0)
        {
            return;
        }

        int increase = newArmySize - armySize;
        for (int i = 0; i < increase; i++)
        {
            enemyArmy.spawnSoldier();
        }
        armySize = newArmySize;
    }

    public void reduceArmySize()
    {
        if (armySize <= 0)
        {
            return;
        }
        Debug.Log("army size reduced: " + --armySize);
        enemyArmy.killSoldier();
    }
}
