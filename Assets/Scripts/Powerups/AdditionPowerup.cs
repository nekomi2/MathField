using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionPowerup : Powerup
{
    void Awake()
    {
        operation = Operation.Addition;
        operand = 10;
        modulus = 1;
        limit = 30;
    }
}
