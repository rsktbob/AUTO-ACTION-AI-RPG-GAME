using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public GameObject leftLeg;
    public GameObject leftFoot;
    public GameObject rightLeg;
    public GameObject rightFoot;
    public GameObject head;
    public GameObject body;

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
