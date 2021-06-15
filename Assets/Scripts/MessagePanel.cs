using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    public GameController gameController;

    public Canvas msgPanel;

    public GameObject questionImage;
    public GameObject doorUnlockedImage;
    public GameObject cameraOffImage;
    public GameObject passwordReceivedImage;
    public GameObject operationFailedImage;

    public float fadeImageSpeed = 0.05f;

    public Vector3 offset;

    Transform player;

    Coroutine crntCoroutine;

    void Start()
    {
        gameController.messagePanel = this;

        if (msgPanel == null)
            Debug.LogError("Не установлена панель для вывода изображений");
    }

    void FixedUpdate()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        transform.position = player.position + offset;
    }

    void DisplayImage(Image img)
    {
        if (img.color.a < 1)
        {
            StopCoroutine(crntCoroutine);
            Color color = img.color;
            color.a = 1;
            img.color = color;
        }

        img.gameObject.SetActive(true);
        crntCoroutine = StartCoroutine(FadeImage(img));
    }

    public void DisplayQuestion()
    {
        DisplayImage(questionImage.GetComponent<Image>());
    }

    public void DisplayDoorUnlocked()
    {
        DisplayImage(doorUnlockedImage.GetComponent<Image>());
    }

    public void DisplayCameraOff()
    {
        DisplayImage(cameraOffImage.GetComponent<Image>());
    }

    public void DisplayPasswordReceived()
    {
        DisplayImage(passwordReceivedImage.GetComponent<Image>());
    }

    public void DisplayOperationFailed()
    {
        DisplayImage(operationFailedImage.GetComponent<Image>());
    }

    IEnumerator FadeImage(Image img)
    {
        while (img.color.a > 0)
        {
            yield return new WaitForSeconds(0.1f);
            Color color = img.color;
            color.a -= fadeImageSpeed;
            img.color = color;
        }
        
        if (img.color.a <= 0)
        {
            Color color = img.color;
            color.a = 1;
            img.color = color;
            img.gameObject.SetActive(false);
        }
    }
}
