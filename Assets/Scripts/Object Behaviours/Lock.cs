using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : ClickableObject
{
    public GameController gameControllerSO;

    public AudioClip unlockSound;
    public AudioClip unlockingDoorSound;
    public AudioClip failSound;

    public string unlockTag = "Pushable";

    public int launchTimes = 1;
    public int btnImgIndex = 0;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void StartAction()
    {
        audioSource.loop = true;
        audioSource.clip = unlockingDoorSound;
        audioSource.Play();
        gameControllerSO.LaunchMiniGame(this, launchTimes, btnImgIndex);
    }

    public override void ResponseFromMiniGame(bool isSuccess)
    {
        if (isSuccess)
        {
            gameControllerSO.messagePanel.DisplayDoorUnlocked();
            Unlock();
        }
        else
        {
            gameControllerSO.messagePanel.DisplayOperationFailed();
            audioSource.Stop();
            audioSource.PlayOneShot(failSound);
        }
    }

    void Unlock()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(unlockSound);
        tag = unlockTag;
        rb.mass = 1;
    }
}
