using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocking : PlayerClickBehaviour
{
    public override void StartAction(GameObject usedObject)
    {
        usedObject.GetComponent<ClickableObject>().StartAction();
    }
}
