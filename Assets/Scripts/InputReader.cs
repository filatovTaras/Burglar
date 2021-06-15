using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// отделил класс чтения ввода данных от Mover, чтобы в будущем Mover использовать в других местах
[RequireComponent(typeof(Mover))]
public class InputReader : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    Mover mover;
    
    void Start()
    {
        mover = GetComponent<Mover>();
    }
    
    void Update()
    {
        // запоминаем координаты нажатия
        if (Input.GetMouseButtonDown(0))
        {
            startPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        // вычисляем направление передвижения при зажатой кнопке мыши
        if (Input.GetMouseButton(0))
        {
            endPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mover.direction = new Vector3(endPos.x - startPos.x, 0, endPos.y - startPos.y);
        }
        // при отжатии кнопки перестаём передвигаться
        if (Input.GetMouseButtonUp(0))
        {
            mover.direction = Vector3.zero;
        }
    }
}
