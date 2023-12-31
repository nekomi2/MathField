using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmy : MonoBehaviour
{
    public EnemySoldier enemySoldierPrefab;
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
        EnemySoldier newSoldier = Instantiate(enemySoldierPrefab, this.transform.position - new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), Quaternion.identity);
        newSoldier.transform.SetParent(this.transform);
        newSoldier.gameObject.SetActive(true);
        newSoldier.enemyArmy = this;
    }
    public void killSoldier()
    {
        EnemySoldier soldier = this.GetComponentInChildren<EnemySoldier>();
        if (soldier != null)
            Destroy(soldier.gameObject);
    }
}
