using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpener : ClickableObject
{
    public GameObject window;

    public override void StartAction()
    {
        window.SetActive(true);
        Time.timeScale = 0;
    }
}
