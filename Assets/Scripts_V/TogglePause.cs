using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePause : MonoBehaviour
{
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image targetButton;

    public void pause() {
        if (targetButton.sprite == buttonSprites[0])
        {
            targetButton.sprite = buttonSprites[1];
            Time.timeScale = 0;
            return;
        }
        targetButton.sprite = buttonSprites[0];
        Time.timeScale = 1;
    }
}
