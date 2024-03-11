using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Body : MonoBehaviour
{
    public bool isTouchFloor = false;
    
    [HideInInspector] 
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
}

