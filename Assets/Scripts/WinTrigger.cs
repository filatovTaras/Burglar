using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public int previousLevel = -1;

    public int sceneSide = 0;

    SceneChanger sceneChanger;

    void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Win();
        }
    }

    void Win()
    {
        if (previousLevel != -1)
            sceneChanger.LoadScene(previousLevel, sceneSide);
        else
            sceneChanger.LoadNextScene(sceneSide);
    }
}
