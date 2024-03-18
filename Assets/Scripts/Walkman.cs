using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Mathematics;
using System;


public enum FootState
{
    Right,
    Left
}

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
                Debug.Log($"left: {(15f - math.abs(0.4f - (RightFoot.transform.position.x - LeftFoot.transform.position.x)) * 60) * 3} {RightFoot.transform.position.x - LeftFoot.transform.position.x}");
                RewardEvent.Invoke((15f - math.abs(0.4f - (RightFoot.transform.position.x - LeftFoot.transform.position.x)) * 60) * 3);
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
                Debug.Log($"right: {(15f - math.abs(0.4f - (LeftFoot.transform.position.x - RightFoot.transform.position.x)) * 60) * 3} {LeftFoot.transform.position.x - RightFoot.transform.position.x}");
                RewardEvent.Invoke((15f - math.abs(0.4f - (LeftFoot.transform.position.x - RightFoot.transform.position.x)) * 60) * 3);
            }
            footstate = FootState.Left;
        }
    }

    public void OnRightFootCollisionLeave()
    {
        if (LeftFoot.isTouchFloor & Chest.transform.localPosition.y > 2.5f & footstate == FootState.Right)
        {
            //Debug.Log("rightup");
            RewardEvent.Invoke(10);
            CancelInvoke("AddLegRotationReward");
            InvokeRepeating("AddLegRotationReward", 0.1f, 0.1f);
        }
    }

    public void OnLeftFootCollisionLeave()
    {
        if (RightFoot.isTouchFloor & Chest.transform.localPosition.y > 2.5f & footstate == FootState.Left)
        {
            //Debug.Log("leftforward");
            RewardEvent.Invoke(10);
            CancelInvoke("AddLegRotationReward");
            InvokeRepeating("AddLegRotationReward", 0.1f, 0.1f);
        }
    }

    // If leg rotation is colser to 45, it can get more point
    private void AddLegRotationReward()
    {
        Func<float, float, float> getRotationReward = (float x1, float x2) =>
        {
            float rotationX = Mathf.DeltaAngle(x1, x2);
            return Mathf.Max(6 - Mathf.Abs(Mathf.DeltaAngle(rotationX, 60)) * 0.6f, -15);
        };
        if (footstate == FootState.Left && LeftFoot.transform.localPosition.y > 1.65f)
        {
            RewardEvent.Invoke(getRotationReward(LeftLeg.transform.eulerAngles.x, LeftUpLeg.transform.eulerAngles.x) * 5);
            CancelInvoke("AddLegRotationReward");
        }
        else if (footstate == FootState.Right && RightFoot.transform.localPosition.y > 1.65f)
        {
            RewardEvent.Invoke(getRotationReward(RightLeg.transform.eulerAngles.x, RightUpLeg.transform.eulerAngles.x) * 5);
            CancelInvoke("AddLegRotationReward");
        }
    }
}
