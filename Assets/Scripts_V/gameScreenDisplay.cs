using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class gameScreenDisplay : MonoBehaviour
{
    public TMP_Text armyCount;
    public TMP_Text opponentCount;
    public TMP_Text timer;
    public GameObject playerObj;
    public GameObject opponentObj;

    public float shrinkIntervals;
    public bool shrink;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        armyCount.SetText("Your army count: " + playerObj.GetComponent<MainPlayer>().armySize);
        opponentCount.SetText("Opponent army count: " + opponentObj.GetComponent<Enemy>()); //currently enemy.cs does not track army size 

        if (shrink && shrinkIntervals > 0)
        {
            shrinkIntervals -= Time.deltaTime;
        }

        if (shrinkIntervals <= 0)
        {
            timer.SetText("ARENA SHRINKING!!");
        }
        else
        {
            timer.SetText("Time until arena shrinks: " + shrinkIntervals);
        }
    }
}
