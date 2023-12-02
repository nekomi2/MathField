using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    public new Camera camera;
    public GameObject army;
    private CharacterController controller;
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();

        // Initialize animator parameters
        anim.SetBool("IsRunningForward", false);
        anim.SetBool("isWalkingForward", false);
        anim.SetBool("isWalkingBackward", false);
        anim.SetBool("isDead", false);
    }

    // Update is called once per frame
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
        anim.SetBool("IsRunningForward", moveVertical > 0 && Input.GetKey(KeyCode.LeftShift));
    }


    public void ChangeArmySize()
    {
    }
}
