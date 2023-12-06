using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public int armySize = 1; // New variable for army size
    public PlayerArmy playerArmy; // Reference to PlayerArmy

    public bool inCombat = false;

    public bool isDead = false;

    public float combatTime = 0.0f;

    public float combatInterval = 0.2f;

    public new Camera camera;
    private CharacterController controller;
    private Animator anim;
    public Enemy EnemyCharacter;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();

        anim.SetBool("isRunningForward", false);
        anim.SetBool("isWalkingForward", false);
        anim.SetBool("isWalkingBackward", false);
        anim.SetBool("isDead", false);
    }

    void Update()
    {
        if (isDead || EnemyCharacter.isDead)
        {
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isAttacking", false);
            if(!isDead){
                anim.SetBool("isWinning", true);
            }
            return;
        }
        updateMovement();
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
    }

    private void updateMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = camera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = camera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 movement = forward * moveVertical * speed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            movement = Vector3.ProjectOnPlane(movement, hit.normal);
        }
        controller.Move(movement);
        if (moveHorizontal != 0)
        {
            transform.Rotate(0, moveHorizontal * rotationSpeed * Time.deltaTime, 0);
        }

        anim.SetBool("isWalkingForward", moveVertical > 0);
        anim.SetBool("isWalkingBackward", moveVertical < 0);
        anim.SetBool("isRunningForward", moveVertical > 0 && Input.GetKey(KeyCode.LeftShift));
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
            playerArmy.spawnSoldier();
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
        playerArmy.killSoldier();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            inCombat = true;
            combatTime = Time.time;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            inCombat = false;
        }
    }

}
