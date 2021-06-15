using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacking : PlayerClickBehaviour
{
    public override void StartAction(GameObject usedObject)
    {
        usedObject.GetComponent<ClickableObject>().StartAction();
    }
}
