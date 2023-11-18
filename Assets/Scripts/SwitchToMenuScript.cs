using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchToMenuScript : MonoBehaviour
{
    void Start(){
        Debug.Log("Activated");
    }
    public void switchToMenu(){
        Debug.Log("Button clicked");
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }

}
