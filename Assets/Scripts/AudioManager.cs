using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameController gameController;

    public List<AudioClip> musics = new List<AudioClip>();

    AudioSource audioSource;

    int crntMusic = 0;

    void Start()
    {
        gameController.audioManager = this;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("StartPlayMusic", .1f);
    }

    IEnumerator StartPlayMusic(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = musics[crntMusic];
                audioSource.Play();
                crntMusic++;

                if (crntMusic >= musics.Capacity)
                    crntMusic = 0;
            }
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
