using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseWindow : MonoBehaviour
{
    public GameController gameController;

    public Transform restartBtn;

    public int btnRotationSpeed = 1;

    bool isStartAnim = false;
    bool isReduceScaleBtn = false;
    float maxBtnScale = 1.2f;
    
    void Update()
    {
        AnimationBtn();
    }

    void Start()
    {
        // регистрируемся в скриптбл обжект 
        if(gameController != null)
            gameController.loseWindow = gameObject;
        gameObject.SetActive(false);
    }

    public void StartAnimationRestartBtn()
    {
        isStartAnim = true;
    }

    void AnimationBtn()
    {
        if (!isStartAnim) return;

        Vector3 rot = restartBtn.rotation.eulerAngles;

        rot.z += btnRotationSpeed;

        restartBtn.rotation = Quaternion.Euler(rot);
        if (!isReduceScaleBtn)
        {
            restartBtn.localScale += new Vector3(0.015f, 0.015f, 0.015f);
            if (restartBtn.localScale.x >= maxBtnScale)
                isReduceScaleBtn = true;
        }
        else
        {
            restartBtn.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
