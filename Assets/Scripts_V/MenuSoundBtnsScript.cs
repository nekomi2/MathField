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

    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(base.gameObject);
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
