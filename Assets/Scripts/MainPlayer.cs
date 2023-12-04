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

        anim.SetBool("isRunningForward", false);
        anim.SetBool("isWalkingForward", false);
        anim.SetBool("isWalkingBackward", false);
        anim.SetBool("isDead", false);
    }

    void Update()
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

        // Use a raycast to find the normal of the terrain
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            // Adjust the movement vector to be perpendicular to the terrain normal
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
