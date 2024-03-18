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
        // 檢查碰撞是否發生在預期的對象上
        if (collision.gameObject.CompareTag("floor"))
        {
            isTouchFloor = true;
            CollisionEnterEvent.Invoke(gameObject.name); // invoke walkman event
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
}
