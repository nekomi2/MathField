using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public GameObject menuSoundControl;

    public float shrinkIntervals;
    public bool shrink;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("PlayerCharacter");
        opponentObj = GameObject.Find("EnemyCharacter");
        menuSoundControl = GameObject.Find("BtnsCanvas");
    }

        // Update is called once per frame
    void Update()
    {
        armyCount.SetText("Your army count: " + playerObj.GetComponent<MainPlayer>().armySize);
        opponentCount.SetText("Opponent army count: " + opponentObj.GetComponent<Enemy>().armySize);
        if (menuSoundControl.GetComponent<MenuSoundBtnsScript>().lost)
        {
            shrink = false;
            timer.SetText("YOU LOST!");
        }
        if (menuSoundControl.GetComponent<MenuSoundBtnsScript>().won)
        {
            shrink = false;
            timer.SetText("YOU WON");
        }

        if (shrink && shrinkIntervals > 0)
        {
            shrinkIntervals -= Time.deltaTime;
            timer.SetText("Time until game ends: " + shrinkIntervals);
        }
        if (shrinkIntervals <= 0)
        {
            if(playerObj.GetComponent<MainPlayer>().armySize >= opponentObj.GetComponent<Enemy>().armySize)
            {
                menuSoundControl.GetComponent<MenuSoundBtnsScript>().won = true;
            }
            else
            {
                menuSoundControl.GetComponent<MenuSoundBtnsScript>().lost = true;
            }

        }
          
    }
 }
