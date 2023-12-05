using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    public string operation;
    public int operand;

    public int modulus;

    public int limit;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainPlayer player = other.GetComponent<MainPlayer>();
            player.increaseArmySize(calculateNewArmySize(player.armySize));
            Destroy(gameObject);
        }
    }

    private int calculateNewArmySize(int armySize)
    {
        Debug.Log("Calculating new army size");
        int newArmySize = 0;
        switch (operation)
        {
            case "addition":
                newArmySize = Mathf.Min(limit, armySize + operand);
                break;
            case "multiplication":
                newArmySize = Mathf.Min(limit, armySize * operand);
                break;
            case "modulus addition":
                newArmySize = Mathf.Min(limit, armySize + operand % modulus);
                break;
            case "modulus multiplication":
                newArmySize = Mathf.Min(limit, armySize * operand % modulus);
                break;
        }
        Debug.Log("New army size is " + newArmySize);
        return newArmySize;
    }
}
