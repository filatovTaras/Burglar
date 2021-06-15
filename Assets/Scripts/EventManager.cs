using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab = default;

    public List<Transform> playerStartPosPrefab = new List<Transform>();

    int playerRespawnPoint;

    void Start()
    {
        LoadSavedLevel();
        PlayerRespawn();
    }

    // проверяем есть ли сохранённый уровень, если есть, то загружаем его
    void LoadSavedLevel()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel")) return;
        if (PlayerPrefs.GetInt("CurrentLevel") == SceneManager.GetActiveScene().buildIndex) return;
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
    }
    
    // Создание персонажа спустя 1 секунду с помощью корутины. Время респауна может быть изменено из инспектора в случае необходимости
    void PlayerRespawn()
    {
        playerRespawnPoint = PlayerPrefs.GetInt("sceneSide", 0);
        Vector3 RespawnPosition = playerStartPosPrefab[playerRespawnPoint].position;
        Instantiate(playerPrefab, RespawnPosition, Quaternion.identity);
    }
}
