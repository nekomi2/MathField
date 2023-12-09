using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitchScript : MonoBehaviour
{
    public void switchToInstructions(){
        SceneManager.LoadScene("InstructionsScene", LoadSceneMode.Single);
    }

    public void switchToGame(){
        SceneManager.LoadScene("Steve_env", LoadSceneMode.Single); //Change this if you're not naming the main game scene "GameScene"
    }

    

}
