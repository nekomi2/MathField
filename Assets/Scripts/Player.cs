using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f; 
    public float rotationSpeed = 100.0f; 
    public int armySize = 1;

    public PlayerArmy playerArmy;

    void Start()
    {
        
    }
    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal != 0)
        {
            transform.Rotate(0, moveHorizontal * rotationSpeed * Time.deltaTime, 0);

        }
    }

    public void increaseArmySize(){
        Debug.Log(++armySize);
        playerArmy.spawnSoldier();
    }
}
