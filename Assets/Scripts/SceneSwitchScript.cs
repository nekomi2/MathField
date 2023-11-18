using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitchScript : MonoBehaviour
{
    public void switchToInstructions(){
        SceneManager.LoadScene("InstructionsScene", LoadSceneMode.Single);
        Debug.Log("Scene Loaded");
    }

    

}
