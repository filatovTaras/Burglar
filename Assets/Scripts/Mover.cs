using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5f; // скорость персонажа
    public float turnSmoothTime = 0.1f; // поворот
    [HideInInspector]
    public Vector3 direction;
    public AudioSource moveSound;

    Animator anim;
    float turnSmoothVelocity;
    CharacterController controller;

    bool isHaveAnim = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        if (anim != null)
            isHaveAnim = true;
    }

    // передвижение персонажа при зажатии из любой точки экрана в любую сторону
    void Update()
    {
        Move();
    }

    void Move()
    {
        // передвигаемся в сторону вычисленного направления
        Vector3 move = new Vector3(direction.x, 0, direction.z);
        move = move.normalized;
        // поворот персонажа
        if (move.magnitude >= 0.1f)
        {
            if (isHaveAnim)
                anim.SetBool("Run", true);
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(move * speed * Time.deltaTime);

            if (moveSound == null) return;
            if (moveSound.isPlaying) return;
            moveSound.PlayOneShot(moveSound.clip);
        }
        else
        {
            if (isHaveAnim)
                anim.SetBool("Run", false);

            if (moveSound == null) return;
            moveSound.Stop();
        }
    }


}
