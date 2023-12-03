using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicationPowerup : Powerup
{
    void Awake()
    {
        operation = "multiplication";
        operand = 2;
        modulus = 10;
        limit = 10;
    }
}