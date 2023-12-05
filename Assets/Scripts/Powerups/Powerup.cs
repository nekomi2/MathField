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
