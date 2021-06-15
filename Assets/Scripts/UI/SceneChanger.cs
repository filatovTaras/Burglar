using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    float waitTime = 1;

    int crntSceneNumber;

    void Start()
    {
        crntSceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator CorutLoadScene(int loadSceneNumber)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(loadSceneNumber);
    }

    public void LoadNextScene(int sceneSide)
    {
        PlayerPrefs.SetInt("CurrentLevel", crntSceneNumber + 1);
        PlayerPrefs.SetInt("sceneSide", sceneSide);
        StartCoroutine(CorutLoadScene(crntSceneNumber + 1));
    }

    public void LoadScene(int SceneIndex, int sceneSide)
    {
        PlayerPrefs.SetInt("CurrentLevel", SceneIndex);
        PlayerPrefs.SetInt("sceneSide", sceneSide);
        StartCoroutine(CorutLoadScene(SceneIndex));
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        StartCoroutine(CorutLoadScene(crntSceneNumber));
    }
}
