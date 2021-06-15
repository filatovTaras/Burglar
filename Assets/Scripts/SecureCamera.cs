using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecureCamera : MonoBehaviour
{
    public Transform vision;
    public Transform cameraCorpuse;

    public MeshRenderer ViewVisualisation;

    public float smoothCameraRot = 0.1f;

    Mover       visionMover;
    Patroler    visionPatroler;
    FieldOfView visionFieldOfView;

    void Start()
    {
        visionMover         = vision.GetComponent<Mover>();
        visionPatroler      = vision.GetComponent<Patroler>();
        visionFieldOfView   = vision.GetComponent<FieldOfView>();

        StartCoroutine("TrackWithDelay", .1f);
    }

    IEnumerator TrackWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            cameraCorpuse.LookAt(vision);
        }
    }

    public void TurnOff()
    {
        visionMover.enabled = false;
        visionPatroler.enabled = false;
        visionFieldOfView.enabled = false;
        ViewVisualisation.enabled = false;
        vision.GetComponent<FieldOfView>().viewAngle = 0;
    }

    public void TurnOn()
    {
        visionMover.enabled = true;
        visionPatroler.enabled = true;
        visionFieldOfView.enabled = true;
        ViewVisualisation.enabled = true;
        vision.GetComponent<FieldOfView>().viewAngle = 360;
    }
}
