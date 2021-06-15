using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : PlayerClickBehaviour
{
    public SkinnedMeshRenderer mesh;

    public string startBoolAnimClip;

    public float startDstToObject = 0.4f;

    CharacterController characterController;
    
    InputReader inputReader;

    Animator anim;

    ClickableObject usedObject;

    bool isHide = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        inputReader = GetComponent<InputReader>();

        if (anim == null)
            Debug.LogError("у объекта " + gameObject.name + " отсутствует компонент Animator");
        if (characterController == null)
            Debug.LogError("у объекта " + gameObject.name + " отсутствует компонент CharacterController");
        if (inputReader == null)
            Debug.LogError("у объекта " + gameObject.name + " отсутствует компонент InputReader");
        if (startBoolAnimClip == "")
            Debug.LogError("Не введено название анимации для Hiding");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isHide)
        {
            CancelHiding();
        }
    }

    public override void StartAction(GameObject usedObject)
    {
        if (isHide) return;

        Vector3 closePos = GetClosePosition(usedObject, startDstToObject);

        if (closePos == Vector3.zero)
            return;

        transform.position = closePos;

        transform.LookAt(usedObject.transform);
        anim.SetBool(startBoolAnimClip, true);
        inputReader.enabled = false;

        if (usedObject.GetComponent<ClickableObject>())
        {
            this.usedObject = usedObject.GetComponent<ClickableObject>();
            this.usedObject.StartAction(transform, false);
        }
    }

    public void Hide()
    {
        mesh.enabled = false;
        characterController.enabled = false;
        isHide = true;
        usedObject.EndAction();
    }

    void CancelHiding()
    {
        usedObject.StartAction(transform, true);
        anim.SetBool(startBoolAnimClip, false);
        mesh.enabled = true;
        characterController.enabled = true;
        inputReader.enabled = true;
        StartCoroutine(HideOff());
    }

    IEnumerator HideOff()
    {
        yield return new WaitForSeconds(.01f);
        isHide = false;
    }
}
