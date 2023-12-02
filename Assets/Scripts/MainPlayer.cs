using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public float speed = 10.0f; 
    public float rotationSpeed = 100.0f; 
    public int armySize = 1; // New variable for army size
    public PlayerArmy playerArmy; // Reference to PlayerArmy

    public new Camera camera;
    private CharacterController controller;
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();

        anim.SetBool("IsRunningForward", false);
        anim.SetBool("isWalkingForward", false);
        anim.SetBool("isWalkingBackward", false);
        anim.SetBool("isDead", false);
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * moveVertical * speed * Time.deltaTime;
        controller.Move(movement);
        if (moveHorizontal != 0)
        {
            transform.Rotate(0, moveHorizontal * rotationSpeed * Time.deltaTime, 0);
        }

        anim.SetBool("isWalkingForward", moveVertical > 0);
        anim.SetBool("isWalkingBackward", moveVertical < 0);
        anim.SetBool("IsRunningForward", moveVertical > 0 && Input.GetKey(KeyCode.LeftShift));
        anim.SetBool("isWalkingBackward", moveVertical < 0);
    }

    public void increaseArmySize()
    {
        armySize++;
        playerArmy.spawnSoldier();
    }

    public void ChangeArmySize()
    {
        // Implementation for changing army size, if needed
    }
}
