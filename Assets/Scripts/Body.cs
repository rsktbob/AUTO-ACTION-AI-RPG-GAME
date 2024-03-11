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
    public UnityEvent<string> CollisionEvent;

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
<<<<<<<< HEAD:Assets/Scripts/bodyCollision.cs
            //if (gameObject.name == "RightFoot")
                //agent.GetComponent<WalkMan1_Agent>().rightFootTouch();
            //if (gameObject.name == "LeftFoot")
                //agent.GetComponent<WalkMan1_Agent>().leftFootTouch();
========
            CollisionEvent.Invoke(gameObject.name);
>>>>>>>> main:Assets/Scripts/Body.cs
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isTouchFloor = false;
<<<<<<<< HEAD:Assets/Scripts/bodyCollision.cs
            //if (gameObject.name == "RightFoot")
                //agent.GetComponent<WalkMan1_Agent>().rightFootLeave();
            //if (gameObject.name == "LeftFoot")
                //agent.GetComponent<WalkMan1_Agent>().leftFootLeave();
========
>>>>>>>> main:Assets/Scripts/Body.cs
        }
    }
}
