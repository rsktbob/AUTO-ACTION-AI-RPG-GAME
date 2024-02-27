using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject LeftToe;

    public GameObject RightUpLeg;
    public GameObject RightLeg;
    public GameObject RightFoot;
    public GameObject RightToe;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 position)
    {
        foreach (Transform childTransform in transform)
        {
            childTransform.localPosition = position;
        }
    }

}
