using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Mathematics;

// hit the ground call event
public class Walkman : MonoBehaviour
{
    public Body Head;

    public Body Chest;
    public Body Waist;

    public Body RightHand;
    public Body RightArm;
    public Body RightShoulder;

    public Body LeftHand;
    public Body LeftArm;
    public Body LeftShoulder;

    public Body Hips;
    public Body LeftUpLeg;
    public Body LeftLeg;
    public Body LeftFoot;

    public Body RightUpLeg;
    public Body RightLeg;
    public Body RightFoot;
    public FootState footstate = FootState.Right;
    public enum FootState
    {
        Right = 0,
        Left = 1
    }

    [HideInInspector]
    public UnityEvent<float> RewardEvent;

    // Start is called before the first frame update
    void Start()
    {
        LeftFoot.CollisionEnterEvent.AddListener(OnBodyCollisionEnter);
        RightFoot.CollisionEnterEvent.AddListener(OnBodyCollisionEnter);
        LeftFoot.CollisionLeaveEvent.AddListener(OnBodyCollisionLeave);
        RightFoot.CollisionLeaveEvent.AddListener(OnBodyCollisionLeave);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Notify the WalkmanAgent on body collision.
    public void OnBodyCollisionEnter(string bodyName)
    {
        switch (bodyName)
        {
            case "LeftFoot":
                OnLeftFootCollisionEnter();
                break;
            case "RightFoot":
                OnRightFootCollisionEnter();
                break;
        }
    }

    public void OnBodyCollisionLeave(string bodyName)
    {
        switch (bodyName)
        {
            case "LeftFoot":
                OnLeftFootCollisionLeave();
                break;
            case "RightFoot":
                OnRightFootCollisionLeave();
                break;
        }
    }
    // Notify the WalkmanAgent on leftFoot collision.
    public void OnLeftFootCollisionEnter()
    {
        if (footstate == FootState.Left)
        {
            if (Chest.transform.localPosition.y > 2.5f)
            {
                Debug.Log("leftforward");
                RewardEvent.Invoke(15f - math.abs(0.4f - (RightFoot.transform.position.x - LeftFoot.transform.position.x)) * 60);
            }
            footstate = FootState.Right;
        }
    }

    public void OnRightFootCollisionEnter()
    {
        if (footstate == FootState.Right)
        {
            if (Chest.transform.localPosition.y > 2.5f)
            {
                Debug.Log("rightforward");
                RewardEvent.Invoke(15f - math.abs(0.4f - (LeftFoot.transform.position.x - RightFoot.transform.position.x)) * 60);
            }
            footstate = FootState.Left;
        }
    }

    public void OnRightFootCollisionLeave()
    {
        if (LeftFoot.isTouchFloor & Chest.transform.localPosition.y > 2.5f & footstate == FootState.Right)
        {
            Debug.Log("rightup");
            RewardEvent.Invoke(10);
        }
    }

    public void OnLeftFootCollisionLeave()
    {
        if (RightFoot.isTouchFloor & Chest.transform.localPosition.y > 2.5f & footstate == FootState.Left)
        {
            Debug.Log("leftforward");
            RewardEvent.Invoke(10);
        }
    }
}
