using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindowBehaviour : PlayerClickBehaviour
{
    public override void StartAction(GameObject usedObject)
    {
        usedObject.GetComponent<ClickableObject>().StartAction();
    }
}
