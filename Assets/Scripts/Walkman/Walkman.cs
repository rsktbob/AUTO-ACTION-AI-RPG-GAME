using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Mathematics;
using System;



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

    private float maxAngle = 0;

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
        //switch (bodyName)
        //{
        //    case "LeftFoot":
        //        OnLeftFootCollisionEnter();
        //        break;
        //    case "RightFoot":
        //        OnRightFootCollisionEnter();
        //        break;
        //}
    }

    public void OnBodyCollisionLeave(string bodyName)
    {
        //switch (bodyName)
        //{
        //    case "LeftFoot":
        //        OnLeftFootCollisionLeave();
        //        break;
        //    case "RightFoot":
        //        OnRightFootCollisionLeave();
        //        break;
        //}
    }

    // Notify the WalkmanAgent on leftFoot collision.
    public void OnLeftFootCollisionEnter()
    {
        if (footstate == FootState.Left)
        {
            if (Chest.transform.localPosition.y > 2.4f)
            {
                CancelInvoke("leftKneeAngle");
                float reward = 20f - (math.abs(0.4f - (RightFoot.transform.position.x - LeftFoot.transform.position.x)) * 70);
                Debug.Log($"left: {reward} {RightFoot.transform.position.x - LeftFoot.transform.position.x}");
                if (reward > 0)
                    reward *= 1 + (0.6f - (60 - maxAngle) * 0.01f);
                RewardEvent.Invoke(reward);
                CancelInvoke("FootTimer");
                InvokeRepeating("FootTimer", 3f, 1f);
            }
            footstate = FootState.Right;
        }
    }

    public void OnRightFootCollisionEnter()
    {
        if (footstate == FootState.Right)
        {
            if (Chest.transform.localPosition.y > 2.4f)
            {
                CancelInvoke("rightKneeAngle");
                float reward = 20f - (math.abs(0.4f - (LeftFoot.transform.position.x - RightFoot.transform.position.x)) * 70);
                Debug.Log($"right: {reward} {LeftFoot.transform.position.x - RightFoot.transform.position.x}");
                if (reward > 0)
                    reward *= 1 + (0.6f - (60 - maxAngle) * 0.01f);
                RewardEvent.Invoke(reward);
                CancelInvoke("FootTimer");
                InvokeRepeating("FootTimer", 3f, 1f);
            }
            footstate = FootState.Left;
        }
    }

    public void OnRightFootCollisionLeave()
    {
        if (LeftFoot.isTouchFloor & Chest.transform.localPosition.y > 2.4f & footstate == FootState.Right)
        {
            //Debug.Log("rightup");
            RewardEvent.Invoke(3);
            CancelInvoke("FootTimer");
            InvokeRepeating("FootTimer", 3f, 1f);
            maxAngle = 0;
            InvokeRepeating("rightKneeAngle", 0.1f, 0.1f);
        }
    }

    public void OnLeftFootCollisionLeave()
    {
        if (RightFoot.isTouchFloor & Chest.transform.localPosition.y > 2.4f & footstate == FootState.Left)
        {
            //Debug.Log("leftforward");
            RewardEvent.Invoke(3);
            CancelInvoke("FootTimer");
            InvokeRepeating("FootTimer", 3f, 1f);
            maxAngle = 0;
            InvokeRepeating("leftKneeAngle", 0.1f, 0.1f);
        }
    }

    private void FootTimer()
    {
        RewardEvent.Invoke(-4);
    }

    private void rightKneeAngle()
    {
        maxAngle = Quaternion.Angle(RightLeg.transform.rotation, RightUpLeg.transform.rotation) > maxAngle ? Quaternion.Angle(RightLeg.transform.rotation, RightUpLeg.transform.rotation) : maxAngle;
    }

    private void leftKneeAngle()
    {
        maxAngle = Quaternion.Angle(LeftLeg.transform.rotation, LeftUpLeg.transform.rotation) > maxAngle ? Quaternion.Angle(LeftLeg.transform.rotation, LeftUpLeg.transform.rotation) : maxAngle;
    }

}
