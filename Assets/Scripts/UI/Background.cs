using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameController gameController;

    void Start()
    {
        // регистрируемся в скриптбл обжект 
        gameController.background = gameObject;
        gameObject.SetActive(false);
    }
}
