using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public GameController gameControllerSO;

    public AudioClip acceptSound;

    public List<GameObject> buttonIcons = new List<GameObject>();

    public RectTransform bar;
    public RectTransform accessBar;
    public RectTransform indicator;

    ClickableObject whoLaunch;

    [Range(5, 10)]
    public float speed = 5;

    public int minSpeed = 5;
    public int maxSpeed = 10;

    AudioSource audioSource;

    float barLenth;
    float startAccessBarPoint;
    float endAccessBarPoint;

    int launchTimes = 0;

    bool isForwardPath = true;
    bool isMove = true;
    bool isCloseMiniGame = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameControllerSO.miniGamePanel = gameObject;
        gameObject.SetActive(false);
    }

    void Update()
    {
        IndicatorMove();
        CancelMiniGame();
    }

    public void Launch(ClickableObject whoLaunch, int launchTimes, int btnImgIndex)
    {
        for (int i = 0; i < buttonIcons.Capacity; i++)
        {
            if (i == btnImgIndex)
            {
                buttonIcons[i].SetActive(true);
                continue;
            }
            buttonIcons[i].SetActive(false);
        }

        this.whoLaunch = whoLaunch;
        this.launchTimes = launchTimes;
        SetStartOptions();
    }

    void CancelMiniGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCloseMiniGame = true;
            StartCoroutine(CloseMiniGame());
        }
    }

    IEnumerator CloseMiniGame()
    {
        yield return new WaitForSeconds(.2f);

        if (isCloseMiniGame)
        {
            whoLaunch.ResponseFromMiniGame(false);
            gameObject.SetActive(false);
        }
    }

    void IndicatorMove()
    {
        if (!isMove) return;

        Vector2 nextPos;

        isForwardPath = IsForwardPath(0, barLenth, indicator.anchoredPosition.x, isForwardPath);

        if (isForwardPath)
            nextPos = (Vector2)indicator.position + new Vector2(speed * Time.deltaTime, 0);
        else
            nextPos = (Vector2)indicator.position - new Vector2(speed * Time.deltaTime, 0);

        indicator.position = nextPos;
    }

    bool IsForwardPath(float startPoint, float endPoint, float indicatorPos, bool crntFwdPath)
    {
        if (indicatorPos > endPoint)
            return false;
        else if (indicatorPos < startPoint)
            return true;

        return crntFwdPath;
    }

    public void CheckAccessBar()
    {
        isCloseMiniGame = false;
        isMove = false;
        launchTimes--;

        if (indicator.anchoredPosition.x > startAccessBarPoint && indicator.anchoredPosition.x < endAccessBarPoint)
        {
            audioSource.PlayOneShot(acceptSound);

            if (launchTimes > 0)
            {
                SetStartOptions();
            }
            else
            {
                whoLaunch.ResponseFromMiniGame(true);
                gameObject.SetActive(false);
            }
        }
        else
        {
            whoLaunch.ResponseFromMiniGame(false);
            gameObject.SetActive(false);
        }
    }

    void SetStartOptions()
    {
        barLenth = bar.rect.width;
        indicator.anchoredPosition = Vector2.zero;
        isForwardPath = true;
        speed = Random.Range(minSpeed, maxSpeed);
        isMove = true;

        float accessBarWidthHalf = accessBar.rect.width / 2;
        float accessBarPosX = Random.Range(0 + accessBarWidthHalf, bar.rect.width - accessBarWidthHalf);
        accessBar.anchoredPosition = new Vector2(accessBarPosX, 0);
        startAccessBarPoint = accessBar.anchoredPosition.x - accessBarWidthHalf;
        endAccessBarPoint = accessBar.anchoredPosition.x + accessBarWidthHalf;
    }
}
