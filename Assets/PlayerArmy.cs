using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmy : MonoBehaviour
{
    public PlayerSoldier playerSoldierPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnSoldier()
    {
        // Instantiate the soldier prefab
        //put it in random position around army
        Debug.Log("Spawning soldier");
        Instantiate(playerSoldierPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}