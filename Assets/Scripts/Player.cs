using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player
    public float rotationSpeed = 100.0f; // Speed of the camera rotation
    public new Camera camera;
    public GameObject army;

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

        transform.Translate(movement, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void ChangeArmySize(){
    }
}
