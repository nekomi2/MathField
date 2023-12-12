using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class NextLevelScript : MonoBehaviour
{
    public GameObject btnsDisplay;
    public GameObject endGameCanvas;
    public bool clicked;

    // Start is called before the first frame update
    void Start()
    {
        btnsDisplay = GameObject.Find("BtnsCanvas");
        endGameCanvas = GameObject.Find("EndGameCanvas");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextLevel()
    {
        clicked = true;
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            SceneManager.LoadScene("Level_2", LoadSceneMode.Single);
        }
        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            SceneManager.LoadScene("Level_3", LoadSceneMode.Single);
        }
        if (SceneManager.GetActiveScene().name == "Level_3")
        {
            SceneManager.LoadScene("Level_4", LoadSceneMode.Single);
        }
        if (SceneManager.GetActiveScene().name == "Level_4")
        {
            SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
        }
    }

    public void retryLevel()
    {
        clicked = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }
}
