using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player
    public float rotationSpeed = 100.0f; // Speed of the camera rotation
    public new Camera camera;
    public GameObject army;
    private CharacterController controller;
    private Animator anim; // Declare the Animator variable

    // Start is called before the first frame update
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

        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * speed * Time.deltaTime;

        // Use the CharacterController to move
        controller.Move(movement);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            toRotation.x = 0; // No rotation around the x-axis
            toRotation.z = 0; // No rotation around the z-axis
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Set animator parameters
        anim.SetBool("isWalkingForward", moveVertical > 0);
        anim.SetBool("IsRunningForward", moveVertical > 0 && Input.GetKey(KeyCode.LeftShift));
        anim.SetBool("isWalkingBackward", moveVertical < 0);
        // 'isDead' remains false until the player dies
    }

    public void ChangeArmySize(){
    }
}
