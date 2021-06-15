using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderObject : ClickableObject
{
    public Transform movableObject;

    public AudioSource audioSource;

    public float partOfDst = -1;
    public float speed = 1.0F;
    public float autoCloseTime = 3;
    public float stopCloseTime = 2;

    Vector3 startPos;
    Vector3 finalPos;
    
    float startTime;
    float journeyLength;

    bool isStartAction = false;
    bool isEndAction = false;
    
    void Start()
    {
        if (movableObject == null)
            Debug.LogError("Не установлен двигаемый объект movableObject");

        startPos = movableObject.transform.position;
    }

    void Update()
    {
        if (isStartAction)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            movableObject.position = Vector3.Lerp(startPos, finalPos, fractionOfJourney);

            if(fractionOfJourney > autoCloseTime)
                EndAction();
        }
        else if (isEndAction)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            movableObject.position = Vector3.Lerp(finalPos, startPos, fractionOfJourney);

            if (fractionOfJourney > stopCloseTime)
                isEndAction = false;
        }
    }

    public override void StartAction(Transform player, bool isHaveFinalPos)
    {
        if(!isHaveFinalPos)
            finalPos = FindFinalPos(player);
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, finalPos);
        isStartAction = true;

        audioSource.Stop();
        audioSource.Play();
    }

    Vector3 FindFinalPos(Transform player)
    {
        float fullDst = (startPos - player.position).magnitude;
        Vector3 finalPos = startPos + (player.position - startPos) * (partOfDst / fullDst);
        finalPos.y = movableObject.position.y;
        return finalPos;
    }

    public override void EndAction()
    {
        startTime = Time.time;
        isStartAction = false;
        isEndAction = true;

        audioSource.Stop();
        audioSource.Play();
    }
}
