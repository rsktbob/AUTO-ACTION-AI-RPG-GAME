using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======
using UnityEngine.Events;
using Unity.Mathematics;
>>>>>>> Stashed changes

public class Walkman : MonoBehaviour
{
    public GameObject Head;

    
    public GameObject Chest;
    public GameObject Waist;

    public GameObject RightHand;
    public GameObject RightArm;
    public GameObject RightShoulder;

    public GameObject LeftHand;
    public GameObject LeftArm;
    public GameObject LeftShoulder;

    public GameObject Hip;
    public GameObject LeftUpLeg;
    public GameObject LeftLeg;
    public GameObject LeftFoot;

<<<<<<< Updated upstream
    public GameObject RightUpLeg;
    public GameObject RightLeg;
    public GameObject RightFoot;

=======
    public Body RightUpLeg;
    public Body RightLeg;
    public Body RightFoot;
    FootState footstate = FootState.Right;
    enum FootState
    {
        Right = 0,
        Left = 1
    }

    [HideInInspector]
    public UnityEvent<float> RewardEvent;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        
=======
        LeftFoot.CollisionEnterEvent.AddListener(OnBodyCollisionEnter);
        RightFoot.CollisionEnterEvent.AddListener(OnBodyCollisionEnter);
        LeftFoot.CollisionLeaveEvent.AddListener(OnBodyCollisionLeave);
        RightFoot.CollisionLeaveEvent.AddListener(OnBodyCollisionLeave);
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

<<<<<<< Updated upstream
    public void Move(Vector3 position)
=======
    // Notify the WalkmanAgent on body collision.
    public void OnBodyCollisionEnter(string bodyName)
>>>>>>> Stashed changes
    {
        foreach (Transform childTransform in transform)
        {
<<<<<<< Updated upstream
            childTransform.localPosition = position;
        }
    }

=======
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
            if (LeftFoot.transform.position.x < RightFoot.transform.position.x & Chest.transform.localPosition.y > 2.9f)
            {
                Debug.Log("leftforward");
                RewardEvent.Invoke(10 - math.abs(1 - (RightFoot.transform.position.x - LeftFoot.transform.position.x)) * 3);
            }
            footstate = FootState.Right;
        }
    }

    public void OnRightFootCollisionEnter()
    {
        if (footstate == FootState.Right)
        {
            if (RightFoot.transform.position.x < LeftFoot.transform.position.x & Chest.transform.localPosition.y > 2.9f)
            {
                Debug.Log("rightforward");
                RewardEvent.Invoke(10 - math.abs(1 - (LeftFoot.transform.position.x - RightFoot.transform.position.x)) * 3);
            }
            footstate = FootState.Left;
        }
    }

    public void OnRightFootCollisionLeave()
    {
        if (LeftFoot.isTouchFloor & Chest.transform.localPosition.y > 2.5f & footstate == FootState.Right)
        {
            Debug.Log("rightup");
            RewardEvent.Invoke(20);
        }
    }

    public void OnLeftFootCollisionLeave()
    {
        if (RightFoot.isTouchFloor & Chest.transform.localPosition.y > 2.5f & footstate == FootState.Left)
        {
            Debug.Log("leftforward");
            RewardEvent.Invoke(20);
        }
    }
>>>>>>> Stashed changes
}
