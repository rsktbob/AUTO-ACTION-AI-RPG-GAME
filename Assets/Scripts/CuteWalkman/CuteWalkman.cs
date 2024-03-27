using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Mathematics;
using System;

public class CuteWalkman : MonoBehaviour
{
    public CuteBody Head;

    public CuteBody Chest;

    public CuteBody RightHand;
    public CuteBody RightArm;

    public CuteBody LeftHand;
    public CuteBody LeftArm;

    public CuteBody Hips;
    public CuteBody LeftLeg;
    public CuteBody LeftFoot;

    public CuteBody RightLeg;
    public CuteBody RightFoot;
    public FootState footstate = FootState.Right;

    [HideInInspector]
    public UnityEvent<float> RewardEvent;

    private bool isLiftFoot = false;

    // Start is called before the first frame update
    void Start()
    {
        LeftFoot.CollisionEnterEvent.AddListener(OnBodyCollisionEnter);
        RightFoot.CollisionEnterEvent.AddListener(OnBodyCollisionEnter);
        LeftFoot.CollisionLeaveEvent.AddListener(OnBodyCollisionLeave);
        RightFoot.CollisionLeaveEvent.AddListener(OnBodyCollisionLeave);
        InvokeRepeating("AddRewardRobot", 0.1f, 0.1f);
        InvokeRepeating("AddRewardLiftFoot", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddRewardRobot()
    {
        float eulerChestX = Chest.transform.eulerAngles.x > 180 ? 360 - Chest.transform.eulerAngles.x : Chest.transform.eulerAngles.x;
        float eulerChestZ = Chest.transform.eulerAngles.z > 180 ? 360 - Chest.transform.eulerAngles.z : Chest.transform.eulerAngles.z;
        float eulerLeftLegJointZ = Mathf.Abs(Mathf.DeltaAngle(Hips.transform.eulerAngles.z, LeftLeg.transform.eulerAngles.z));
        float eulerRightLegJointZ = Mathf.Abs(Mathf.DeltaAngle(Hips.transform.eulerAngles.z, RightLeg.transform.eulerAngles.z));
        float eulerLeftArmJointZ = Mathf.Abs(Mathf.DeltaAngle(Chest.transform.eulerAngles.z, LeftArm.transform.eulerAngles.z));
        float eulerRightArmJointZ = Mathf.Abs(Mathf.DeltaAngle(Chest.transform.eulerAngles.z, RightArm.transform.eulerAngles.z));
        float eulerHipsX = Hips.transform.eulerAngles.x > 180 ? 360 - Hips.transform.eulerAngles.x : Hips.transform.eulerAngles.x;
        float eulerHipsZ = Hips.transform.eulerAngles.z > 180 ? 360 - Hips.transform.eulerAngles.z : Hips.transform.eulerAngles.z;
        float hipsPositionX = Mathf.Abs((Hips.transform.localPosition.x));
        RewardEvent.Invoke((20 - eulerChestX) / 300);
        RewardEvent.Invoke((20 - eulerChestZ) / 300);
        RewardEvent.Invoke((20 - eulerLeftLegJointZ) / 300);
        RewardEvent.Invoke((20 - eulerRightLegJointZ) / 300);
        RewardEvent.Invoke((20 - eulerChestX) / 300);
        RewardEvent.Invoke((15 - eulerHipsX) / 10);
        RewardEvent.Invoke((15 - eulerHipsZ) / 10);
        RewardEvent.Invoke((30 - eulerLeftArmJointZ) / 100);
        RewardEvent.Invoke((30 - eulerRightArmJointZ) / 100);
        RewardEvent.Invoke(1 - hipsPositionX);

        float height = Chest.transform.localPosition.y;
        RewardEvent.Invoke((height - 2) / 100);
        Debug.Log($"hips {Hips.transform.localPosition.x} {1 - hipsPositionX}");
    }

    private void AddRewardLiftFoot()
    {
        if (!isLiftFoot)
        {
            RewardEvent.Invoke(-100);
        }
        else
        {
            isLiftFoot = false;
            RewardEvent.Invoke(10);
        }
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
            if (Chest.transform.localPosition.y > 2f)
            {
                //Debug.Log($"left: {(10f - math.abs(0.2f - (RightFoot.transform.position.x - LeftFoot.transform.position.x)) * 240)} {RightFoot.transform.position.x - LeftFoot.transform.position.x}");
                RewardEvent.Invoke((10f - math.abs(0.2f - (RightFoot.transform.position.x - LeftFoot.transform.position.x)) * 240));
            }
            footstate = FootState.Right;
        }
    }

    public void OnRightFootCollisionEnter()
    {
        if (footstate == FootState.Right)
        {
            if (Chest.transform.localPosition.y > 2f)
            {
                //Debug.Log($"right: {(10f - math.abs(0.2f - (LeftFoot.transform.position.x - RightFoot.transform.position.x)) * 240)} {LeftFoot.transform.position.x - RightFoot.transform.position.x}");
                RewardEvent.Invoke((10f - math.abs(0.2f - (LeftFoot.transform.position.x - RightFoot.transform.position.x)) * 240));
            }
            footstate = FootState.Left;
        }
    }

    public void OnRightFootCollisionLeave()
    {
        isLiftFoot = true;
        if (LeftFoot.isTouchFloor & Chest.transform.localPosition.y > 2f & footstate == FootState.Right)
        {
            RewardEvent.Invoke(10);
        }
    }

    public void OnLeftFootCollisionLeave()
    {
        isLiftFoot = true;
        if (RightFoot.isTouchFloor & Chest.transform.localPosition.y > 2f & footstate == FootState.Left)
        {
            RewardEvent.Invoke(10);
        }
    }
}
