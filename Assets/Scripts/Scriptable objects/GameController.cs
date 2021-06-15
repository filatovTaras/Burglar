using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameController", menuName = "ScriptableObjects/GameController")]
public class GameController : ScriptableObject
{
    // тут будут храниться окна и тёмный фон
    public GameObject loseWindow;
    public GameObject background;
    public GameObject miniGamePanel;
    public MessagePanel messagePanel;
    public AudioManager audioManager;

    public bool Lose()
    {
        if (loseWindow == null) return false;

        // вызов окна Game Over
        background.SetActive(true);
        loseWindow.SetActive(true);

        background = null;
        loseWindow = null;

        Time.timeScale = 0f;

        return true;
    }

    public void LaunchMiniGame(ClickableObject whoLaunch, int launchTimes, int btnImgIndex)
    {
        miniGamePanel.SetActive(true);
        miniGamePanel.GetComponent<MiniGame>().Launch(whoLaunch, launchTimes, btnImgIndex);
    }
}
