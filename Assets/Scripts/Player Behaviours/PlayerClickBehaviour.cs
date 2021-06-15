using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickBehaviour : MonoBehaviour
{

    public virtual void StartAction(GameObject usedObject)
    {
    }

    protected Vector3 GetCloseSidePosition(Transform target)
    {
        Vector3 startRay = transform.position;
        startRay.y += .5f;
        RaycastHit hit;
        Vector3 direction = (target.position - transform.position).normalized;
        if(Physics.Raycast(startRay, direction, out hit))
        {
            if (hit.transform != target)
                return Vector3.zero;

            return hit.point;
        }
        else
        {
            Debug.LogError("Объект " + target.name + " не найден Рейкастом");
            return Vector3.zero;
        }
    }

    protected Vector3 GetClosePosition(GameObject usedObject, float startDstToObject)
    {
        Vector3 closeSidePosition = GetCloseSidePosition(usedObject.transform);
        if (closeSidePosition == Vector3.zero)
            return Vector3.zero;
        closeSidePosition.y = transform.position.y;

        float fullDst = (closeSidePosition - transform.position).magnitude;
        Vector3 closePos = closeSidePosition + (transform.position - closeSidePosition) * (startDstToObject / fullDst);
        return closePos;
    }
}
