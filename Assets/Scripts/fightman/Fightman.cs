using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Mathematics;




public class Fightman : MonoBehaviour
{
    public FightBody Head;

    public FightBody Chest;
    public FightBody Waist;

    public FightBody RightHand;
    public FightBody RightArm;
    public FightBody RightShoulder;

    public FightBody LeftHand;
    public FightBody LeftArm;
    public FightBody LeftShoulder;

    public FightBody Hips;
    public FightBody LeftUpLeg;
    public FightBody LeftLeg;
    public FightBody LeftFoot;

    public FightBody RightUpLeg;
    public FightBody RightLeg;
    public FightBody RightFoot;
    public Sword Sword;
    public FightFootState footstate = FightFootState.Right;

    public enum FightFootState
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
        Sword.SwordRewardEvent.AddListener(SwordReward);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Chest.transform.eulerAngles);
    }

    public void SwordReward(float value)
    {
        if (Chest.transform.localPosition.y > 2.5f)
        {
            RewardEvent.Invoke(value);
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
        if (footstate == FightFootState.Left)
        {
            if (Chest.transform.localPosition.y > 2.5f)
            {
                Debug.Log("leftforward");
                //RewardEvent.Invoke(15f - math.abs(0.4f - (RightFoot.transform.position.x - LeftFoot.transform.position.x)) * 60);
            }
            footstate = FightFootState.Right;
        }
    }

    public void OnRightFootCollisionEnter()
    {
        if (footstate == FightFootState.Right)
        {
            if (Chest.transform.localPosition.y > 2.5f)
            {
                Debug.Log("rightforward");
                //RewardEvent.Invoke(15f - math.abs(0.4f - (LeftFoot.transform.position.x - RightFoot.transform.position.x)) * 60);
            }
            footstate = FightFootState.Left;
        }
    }

    public void OnRightFootCollisionLeave()
    {
        if (LeftFoot.isTouchFloor & Chest.transform.localPosition.y > 2.5f & footstate == FightFootState.Right)
        {
            Debug.Log("rightup");
            //RewardEvent.Invoke(10);
        }
    }

    public void OnLeftFootCollisionLeave()
    {
        if (RightFoot.isTouchFloor & Chest.transform.localPosition.y > 2.5f & footstate == FightFootState.Left)
        {
            Debug.Log("leftforward");
            //RewardEvent.Invoke(10);
        }
    }
}
