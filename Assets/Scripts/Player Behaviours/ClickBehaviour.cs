using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBehaviour : MonoBehaviour
{
    public GameController gameController;

    public AudioClip questionSound;

    public List<string> clickableTags = new List<string>();

    public List<PlayerClickBehaviour> behaviours = new List<PlayerClickBehaviour>();

    public float availableDstToObject = 1;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChooseBehaviour();
        }
    }

    void ChooseBehaviour()
    {
        if(clickableTags.Capacity != behaviours.Capacity)
        {
            Debug.LogError("Количество тегов для клика должно совпадать с количеством поведений игрока");
            return;
        }

        GameObject clickedObject = GetClickedObject();
        if (clickedObject == null) return;

        if (!IsPlayerNear(clickedObject.transform.position))
        {
            audioSource.PlayOneShot(questionSound);
            gameController.messagePanel.DisplayQuestion();
            return;
        }

        int i = clickableTags.IndexOf(clickedObject.tag);

        if(behaviours[i] == null)
        {
            Debug.LogError("Отсутствует скрипт behaviour в листе под индексом " + i);
            return;
        }

        behaviours[i].StartAction(clickedObject);
    }

    GameObject GetClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(clickableTags.Capacity == 0)
            {
                Debug.LogError("Лист кликабельных тегов пуст.");
                return null;
            }

            GameObject clickedObject = hit.transform.gameObject;

            int i = clickableTags.IndexOf(clickedObject.tag);
            if (i == -1)
                return null;
            else
                return clickedObject;
        }
        else
            return null;
    }

    bool IsPlayerNear(Vector3 clickableObjectPos)
    {
        if (Vector3.Distance(transform.position, clickableObjectPos) < availableDstToObject)
            return true;
        else
            return false;
    }
}
