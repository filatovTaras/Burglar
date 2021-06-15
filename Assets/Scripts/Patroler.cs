using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Patroler : MonoBehaviour
{
    // тут будут храниться все точки патрулирования которые нужно обойти
    [SerializeField]
    List<Transform> patrolPositions = new List<Transform>();
    [SerializeField]
    List<float> waitTimers = new List<float>();

    public float rotationSpeed = 0.1f;

    public float waitSeconds = -1;

    public bool reversePath = false; // чтобы понимать двигается ли объект в обратном направлении

    Mover mover;

    int indexPosition;

    float waitComplete;

    void Start()
    {
        transform.position = patrolPositions[0].position; // начинаем движение с нулевого элемента массива
        indexPosition = 0;
        mover = GetComponent<Mover>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        Vector3 crntPos = patrolPositions[indexPosition].position;
        
        if (IsArriveToPosition(crntPos))
        {
            if (IsHoldPosition(waitTimers, indexPosition))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, patrolPositions[indexPosition].rotation, rotationSpeed);
                MoveToNextPos(transform.position, true);
                return;
            }

            reversePath = IsReversePath(patrolPositions, indexPosition, reversePath);
            indexPosition = GetNewIndexPosition(patrolPositions, indexPosition, reversePath);    
        }

        MoveToNextPos(patrolPositions[indexPosition].position, false);
    }

    // проверяем дошли ли до позиции nextPos
    bool IsArriveToPosition(Vector3 pos)
    {
        float x = (transform.position - pos).sqrMagnitude;
        
        if (x < 0.05f)
            return true;

        return false;
    }

    //определяем следующую позицию куда идти
    int GetNewIndexPosition(List<Transform> listPos, int crntIndexPos, bool reversePath)
    {
        if (listPos.Count <= 1) return -1;

        // определяем следущую позицию
        if (!reversePath)
            return crntIndexPos + 1;
        else
            return crntIndexPos - 1;
    }

    bool IsReversePath(List<Transform> listPos, int crntIndexPos, bool reversePath)
    {
        if (!reversePath)
        {
            if (crntIndexPos + 1 >= listPos.Capacity)
                return true;
            else
                return false;
        }
        else
        {
            if (crntIndexPos - 1 < 0)
                return false;
            else
                return true;
        }
    }

    void MoveToNextPos(Vector3 pos, bool isStop)
    {
        if (isStop)
            mover.direction = Vector3.zero;

        mover.direction = pos - transform.position;
    }

    bool IsHoldPosition(List<float> waitTimers, int crntPos)
    {
        if (waitTimers.Capacity <= crntPos)
            return false;

        if (waitSeconds == -1)
        {
            waitSeconds = waitTimers[crntPos];
            waitComplete = Time.time + waitSeconds;
        }

        if(Time.time > waitComplete)
        {
            waitSeconds = -1;
            return false;
        }

        waitSeconds = waitComplete - Time.time;
        return true;
    }
}
