using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    int isNowFootRight = 0;

    [HideInInspector]
    public UnityEvent<float> RewardEvent;

    // Start is called before the first frame update
    void Start()
    {
        LeftFoot.CollisionEvent.AddListener(OnBodyCollision);
        RightFoot.CollisionEvent.AddListener(OnBodyCollision);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Notify the WalkmanAgent on body collision.
    public void OnBodyCollision(string bodyName)
    {
        switch (bodyName)
        {
            case "LeftFoot":
                OnLeftFootCollision();
                break;
            case "RightFoot":
                OnRightFootCollision();
                break;
        }
    }

    // Notify the WalkmanAgent on leftFoot collision.
    public void OnLeftFootCollision()
    {
        if (isNowFootRight == 0)
        {
            isNowFootRight = 1;
            return;
        }

        if (isNowFootRight == -1)
        {
            if (Chest.transform.localPosition.y > 2.9f)
            {
                Debug.Log("left+");
                isNowFootRight = 1;
                RewardEvent.Invoke(RightFoot.transform.position.x - LeftFoot.transform.position.x);
            }
        }
        else
        {
            if (Chest.transform.localPosition.y > 2.9f)
            {
                Debug.Log("left-");
                RewardEvent.Invoke(-5);
            }
        }
    }

    // Notify the WalkmanAgent on rightFoot collision.
    public void OnRightFootCollision()
    {
        if (isNowFootRight == 0)
        {
            isNowFootRight = -1;
            return;
        }
        if (isNowFootRight == 1)
        {
            if (Chest.transform.localPosition.y > 2.9f)
            {
                Debug.Log("right+");
                isNowFootRight = -1;
                RewardEvent.Invoke(LeftFoot.transform.position.x - RightFoot.transform.position.x);
            }
        }
        else
        {
            if (Chest.transform.localPosition.y > 2.9f)
            {
                Debug.Log("right-");
                RewardEvent.Invoke(5);
            }
        }
    }
}
