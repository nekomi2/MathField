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
        PlayerSoldier newSoldier = Instantiate(playerSoldierPrefab, this.transform.position - new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), Quaternion.identity);
        newSoldier.transform.SetParent(this.transform);
        newSoldier.playerArmy = this;
    }

    public void killSoldier()
    {
        PlayerSoldier soldier = this.GetComponentInChildren<PlayerSoldier>();
        if (soldier != null)
            Destroy(soldier.gameObject);
    }
}
