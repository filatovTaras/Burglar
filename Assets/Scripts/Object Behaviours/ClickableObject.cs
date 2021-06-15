using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public virtual void StartAction()
    {
    }

    public virtual void StartAction(Transform player, bool isHaveFinalPos)
    {
    }

    public virtual void EndAction()
    {
    }

    public virtual void ResponseFromMiniGame(bool isSuccess)
    {
    }
}
