using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Body : MonoBehaviour
{
    [HideInInspector]
    public bool isTouchFloor = false;

    [Header("Event")]
    public UnityEvent<string> CollisionEnterEvent;
    public UnityEvent<string> CollisionLeaveEvent;

    private void Start()
    {
        
    }

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I���O�_�o�ͦb�w������H�W
        if (collision.gameObject.CompareTag("floor"))
        {
            isTouchFloor = true;
            CollisionEnterEvent.Invoke(gameObject.name);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isTouchFloor = false;
            CollisionLeaveEvent.Invoke(gameObject.name);
        }
    }

    // Change body angular velocity
    public void AddAngularVelocity(float xLimit, float yLimit, float zLimit, Vector3 acceleration)
    {
        ConfigurableJoint joint = GetComponent<ConfigurableJoint>();
        float newSpeedX = Mathf.Clamp(joint.targetAngularVelocity.x + acceleration.x, -xLimit, xLimit);
        float newSpeedY = Mathf.Clamp(joint.targetAngularVelocity.y + acceleration.y, -yLimit, yLimit);
        float newSpeedZ = Mathf.Clamp(joint.targetAngularVelocity.z + acceleration.z, -zLimit, zLimit);
        joint.targetAngularVelocity = new Vector3(newSpeedX, newSpeedY, newSpeedZ);
    }
    public void AddAngularVelocity(float xLimit, float yLimit, float zLimit, Vector3 acceleration, int num)
    {
        ConfigurableJoint[] joint = GetComponents<ConfigurableJoint>();
        float newSpeedX = Mathf.Clamp(joint[num].targetAngularVelocity.x + acceleration.x, -xLimit, xLimit);
        float newSpeedY = Mathf.Clamp(joint[num].targetAngularVelocity.y + acceleration.y, -yLimit, yLimit);
        float newSpeedZ = Mathf.Clamp(joint[num].targetAngularVelocity.z + acceleration.z, -zLimit, zLimit);
        joint[num].targetAngularVelocity = new Vector3(newSpeedX, newSpeedY, newSpeedZ);
    }
    public void AddRotation(Vector3 acceleration)
    {
        ConfigurableJoint joint = GetComponent<ConfigurableJoint>();
        float newRotationX = joint.targetRotation.eulerAngles.x + acceleration.x;
        float newRotationY = joint.targetRotation.eulerAngles.y + acceleration.y;
        float newRotationZ = joint.targetRotation.eulerAngles.z + acceleration.z;

        joint.targetRotation = Quaternion.Euler(newRotationX, newRotationY, newRotationZ);
    }

    public void AddRotation(Vector3 acceleration, int num)
    {
        ConfigurableJoint[] joint = GetComponents<ConfigurableJoint>();
        float newRotationX = joint[num].targetRotation.eulerAngles.x + acceleration.x;
        float newRotationY = joint[num].targetRotation.eulerAngles.y + acceleration.y;
        float newRotationZ = joint[num].targetRotation.eulerAngles.z + acceleration.z;

        joint[num].targetRotation = Quaternion.Euler(newRotationX, newRotationY, newRotationZ);
    }
}
