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

    public float speed = 12.0f; //Slightly faster than player

    private void Awake()
    {
        player = GameObject.Find("PlayerCharacter").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
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
            if (!isDead)
            {
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
        if (armySize <= playerCharacter.armySize || !playerInSightRange)
        {
            if (powerUps.Length > 0)
            {
                collectPowerUps(powerUps);
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


private void collectPowerUps(GameObject[] powerUps)
{
    GameObject closestPowerUp = null;
    float closestDistance = Mathf.Infinity;

    foreach (GameObject powerUp in powerUps)
    {
        float distance = Vector3.Distance(transform.position, powerUp.transform.position);
        if (distance < closestDistance)
        {
            closestDistance = distance;
            closestPowerUp = powerUp;
        }
    }

    if (closestPowerUp != null)
    {
        Debug.Log(closestPowerUp.transform.position);

        Rigidbody powerUpRigidbody = closestPowerUp.GetComponent<Rigidbody>();

        // Check if the power-up has a Rigidbody component and is moving
        if (powerUpRigidbody != null && powerUpRigidbody.velocity.magnitude > 0)
        {
            float timeToReach = closestDistance / agent.speed;

            Vector3 predictedPosition = closestPowerUp.transform.position + powerUpRigidbody.velocity * timeToReach;

            agent.SetDestination(predictedPosition);
        }
        else
        {
            agent.SetDestination(closestPowerUp.transform.position);
        }
    }
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

    public bool GetInCombat()
    {
        return inCombat;
    }
}

