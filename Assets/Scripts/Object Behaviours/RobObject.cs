using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobObject : ClickableObject
{
    public GameController gameControllerSO;

    public AudioClip takeList;
    public AudioClip finding;
    public AudioClip failSound;

    public List<CameraControlPanel> cntrlPanels = new List<CameraControlPanel>();

    public int launchTimes = 1;
    public int btnImgIndex = 2;


    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void StartAction()
    {
        gameControllerSO.LaunchMiniGame(this, launchTimes, btnImgIndex);
        
        audioSource.loop = true;
        audioSource.clip = finding;
        audioSource.Play();
    }

    public override void ResponseFromMiniGame(bool isSuccess)
    {
        if (isSuccess)
        {
            gameControllerSO.messagePanel.DisplayPasswordReceived();
            OpenAccessToControlPanels();
        }
        else
        {
            gameControllerSO.messagePanel.DisplayOperationFailed();
            audioSource.Stop();
            audioSource.PlayOneShot(failSound);
        }
    }

    void OpenAccessToControlPanels()
    {
        foreach(CameraControlPanel cntrlPanel in cntrlPanels)
        {
            cntrlPanel.InsertPassword();
        }
        
        audioSource.Stop();
        audioSource.PlayOneShot(takeList);
    }
}
