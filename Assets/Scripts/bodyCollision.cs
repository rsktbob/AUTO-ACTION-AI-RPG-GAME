using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class bodyCollision : MonoBehaviour
{
    public bool isTouchFloor = false;
    private GameObject agent;
    private void Start()
    {
        agent = transform.parent.gameObject.transform.parent.gameObject;
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞是否發生在預期的對象上
        if (collision.gameObject.CompareTag("floor"))
        {
            isTouchFloor = true;
            if (gameObject.name == "RightFoot")
                agent.GetComponent<WalkMan1_Agent>().rightFootTouch();
            if (gameObject.name == "LeftFoot")
                agent.GetComponent<WalkMan1_Agent>().leftFootTouch();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isTouchFloor = false;
            if (gameObject.name == "RightFoot")
                agent.GetComponent<WalkMan1_Agent>().rightFootLeave();
            if (gameObject.name == "LeftFoot")
                agent.GetComponent<WalkMan1_Agent>().leftFootLeave();
        }

    }
}
