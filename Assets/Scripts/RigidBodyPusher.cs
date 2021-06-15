using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPusher : MonoBehaviour
{
    public string pushableTag = "Pushable";
    public float pushPower = 2.0F;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        
        if (hit.transform.gameObject.tag != pushableTag)
            return;
        if (body == null || body.isKinematic)
            return;
        //if (hit.moveDirection.y < -0.3f)
            //return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
