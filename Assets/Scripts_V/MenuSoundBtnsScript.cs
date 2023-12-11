using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuSoundBtnsScript : MonoBehaviour
{

    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image targetButton;

    private AudioSource themeSource;
    private AudioClip theme;

    private AudioSource combatSource;
    private AudioClip combatClip;

    private AudioSource powerUpSource;
    private AudioClip powerUpClip;

    private AudioSource winSource;
    private AudioClip winClip;

    private AudioSource loseSource;
    private AudioClip loseClip;

    public static MenuSoundBtnsScript instance = null;

    public GameObject player;
    public GameObject enemy;
    public GameObject gameUI;

    public bool lost = false;
    public bool won = false;
    public bool gameEnd = false;

    public GameObject endGameCanvas;

    private void Start()
    {
        themeSource = gameObject.AddComponent<AudioSource>();
        theme = Resources.Load<AudioClip>("theduel");
        themeSource.loop = true;
        themeSource.clip = theme;
        themeSource.Play();

        combatSource = gameObject.AddComponent<AudioSource>();
        combatClip = Resources.Load<AudioClip>("slash");

        powerUpSource = gameObject.AddComponent<AudioSource>();
        powerUpClip = Resources.Load<AudioClip>("powerup");

        winSource = gameObject.AddComponent<AudioSource>();
        winClip = Resources.Load<AudioClip>("win");

        loseSource = gameObject.AddComponent<AudioSource>();
        loseClip = Resources.Load<AudioClip>("lose");

        if (!gameEnd)
        {
            endGameCanvas = GameObject.Find("EndGameCanvas");

            if (endGameCanvas != null)
            {
                endGameCanvas.GetComponent<Canvas>().enabled = false;
            }

            lost = false;
            won = false;
            gameEnd = false;
        }

    }

    private void Awake()
    {
        endGameCanvas = GameObject.Find("EndGameCanvas");
        if(endGameCanvas != null)
        {
            endGameCanvas.GetComponent<Canvas>().enabled = false;
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(base.gameObject);
        }
    }

    private void Update()
    {
        player = GameObject.Find("PlayerCharacter");
        enemy = GameObject.Find("EnemyCharacter");
        gameUI = GameObject.Find("GameSceneUI");
        endGameCanvas = GameObject.Find("EndGameCanvas");

        bool inCombat = false;

        if (player != null && enemy != null && !gameEnd)
        {
            inCombat = enemy.GetComponent<Enemy>().GetInCombat();
            if (inCombat)
            {
                playCombatSound();
            }

            if (!lost && player.GetComponent<MainPlayer>().isDead)
            {
                playLose();
                lost = true;
                inCombat = false;
                gameEnd = true;
            }

            if(!won && enemy.GetComponent<Enemy>().isDead) {
                playWin();
                won = true;
                inCombat = false;
                gameEnd=true;
            }
        }
        if(gameUI != null && !gameEnd)
        {
            endGameCanvas.GetComponent<Canvas>().enabled = false;
            lost = false;
            won = false;
            gameEnd = false;

            if (gameUI.GetComponent<gameScreenDisplay>().shrinkIntervals <= 0)
            {
                Debug.Log("Time ran out");
                if (player.GetComponent<MainPlayer>().armySize >= enemy.GetComponent<Enemy>().armySize)
                {

                    if (winSource.isPlaying)
                    {
                        return;
                    }
                    playWin();
                    won = true;
                    inCombat = false;
                    gameEnd = true;
                }
                else
                {
                    if (loseSource.isPlaying) { return; }
                    playLose();
                    lost = true;
                    inCombat = false;
                    gameEnd = true;
                }
            }
        }
        if (gameEnd)
        {


            if(!winSource.isPlaying && !loseSource.isPlaying)
            {
                endGameCanvas = GameObject.Find("EndGameCanvas");
                endGameCanvas.GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0;
                themeSource.Play();
            }
        }

    }



    public void playPauseTheme(){
        if(targetButton.sprite == buttonSprites[0]){
            targetButton.sprite = buttonSprites[1];
            AudioListener.volume = 1;
            return;
        }
        targetButton.sprite = buttonSprites[0];
        AudioListener.volume = 0;


    }

    public void playCombatSound()
    {
        if (combatSource.isPlaying)
        {
            return;
        }
        combatSource.PlayOneShot(combatClip);
    }

    public void playPowerUp()
    {

        powerUpSource.PlayOneShot(powerUpClip);
    }

    public void playWin() {
        themeSource.Stop();
        winSource.PlayOneShot(winClip);
    }

    public void playLose()
    {
        themeSource.Stop();
        loseSource.PlayOneShot(loseClip);
    }
}
