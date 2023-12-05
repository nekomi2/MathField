using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicationPowerup : Powerup
{
    void Awake()
    {
        operation = Operation.Multiplication;
        operand = 2;
        modulus = 1;
        limit = 30;
    }
}