using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlPanel : ClickableObject
{
    public GameController gameControllerSO;

    public AudioClip switchOffSound;
    public AudioClip pushingButtons;
    public AudioClip failSound;

    public List<SecureCamera> secureCameras = new List<SecureCamera>();

    public int launchTimes = 1;
    public int btnImgIndex = 1;

    public string neutralTag = "Untagged";
    public string clickTag = "CamCtrlPanel";

    public bool isHavePassword = false;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void StartAction()
    {
        if (isHavePassword)
        {
            gameControllerSO.messagePanel.DisplayPasswordReceived();
            return;
        }
        audioSource.loop = true;
        audioSource.clip = pushingButtons;
        audioSource.Play();

        gameControllerSO.LaunchMiniGame(this, launchTimes, btnImgIndex);
    }

    public void InsertPassword()
    {
        isHavePassword = false;
        gameObject.tag = clickTag;
    }

    public override void ResponseFromMiniGame(bool isSuccess)
    {
        if (isSuccess)
        {
            gameControllerSO.messagePanel.DisplayCameraOff();
            SwitchOff();
        }
        else
        {
            gameControllerSO.messagePanel.DisplayOperationFailed();
            audioSource.Stop();
            audioSource.PlayOneShot(failSound);
        }
    }

    public void SwitchOff()
    {
        foreach(SecureCamera cam in secureCameras)
        {
            cam.TurnOff();
        }
        gameObject.tag = neutralTag;

        audioSource.Stop();
        audioSource.PlayOneShot(switchOffSound);
    }

    public void SwitchOn()
    {
        foreach (SecureCamera cam in secureCameras)
        {
            cam.TurnOn();
        }
    }
}
