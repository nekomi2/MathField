using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchToMenuScript : MonoBehaviour
{
    public void switchToMenu(){
        if (SceneManager.GetActiveScene().name == "IntroScene")
        {
            return;
        }
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }

}
