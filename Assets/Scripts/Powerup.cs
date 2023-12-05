using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operation
{
    Addition,
    Multiplication,
    ModulusAddition,
    ModulusMultiplication
}

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    public Operation operation;
    public int operand;

    public int modulus;

    public int limit;

    private TMPro.TextMeshProUGUI[] texts;

    void Awake()
    {
        //Put on surface normal
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.position = hit.point;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }
    void Start()
    {
        texts = GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);
        foreach (TMPro.TextMeshProUGUI t in texts)
        {
            string operationString = "";
            switch (operation)
            {
                case Operation.Addition:
                case Operation.ModulusAddition:
                    operationString = "+";
                    break;
                case Operation.Multiplication:
                case Operation.ModulusMultiplication:
                    operationString = "x";
                    break;
            }
            string modulusString = (operation == Operation.ModulusAddition || operation == Operation.ModulusMultiplication) ? "\n% " + modulus : "";
            t.text = operationString + " " + operand.ToString() + modulusString;
        }
    }

    public void createAdditionPowerup(int operand, int limit)
    {
        operation = Operation.Addition;
        this.operand = operand;
        this.limit = limit;
    }

    public void createMultiplicationPowerup(int operand, int limit)
    {
        operation = Operation.Multiplication;
        this.operand = operand;
        this.limit = limit;
    }

    public void createModulusAdditionPowerup(int operand, int modulus, int limit)
    {
        operation = Operation.ModulusAddition;
        this.operand = operand;
        this.modulus = modulus;
        this.limit = limit;
    }

    public void createModulusMultiplicationPowerup(int operand, int modulus, int limit)
    {
        operation = Operation.ModulusMultiplication;
        this.operand = operand;
        this.modulus = modulus;
        this.limit = limit;
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
        int newArmySize = 0;
        switch (operation)
        {
            case Operation.Addition:
                newArmySize = Mathf.Min(limit, armySize + operand);
                break;
            case Operation.Multiplication:
                newArmySize = Mathf.Min(limit, armySize * operand);
                break;
            case Operation.ModulusAddition:
                newArmySize = Mathf.Min(limit, armySize + operand % modulus);
                break;
            case Operation.ModulusMultiplication:
                newArmySize = Mathf.Min(limit, armySize * operand % modulus);
                break;
        }
        Debug.Log("New army size is " + newArmySize);
        return newArmySize;
    }
}
