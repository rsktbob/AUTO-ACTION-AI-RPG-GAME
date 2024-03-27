using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;
using Unity.Mathematics;
using UnityEditor;

public class CuteWalkmanAgent : Agent
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private float rewardUpperLimit;
    private CuteWalkman robot;
    int count = 0;

    // reset the agent or remove it from the environment
    public override void OnEpisodeBegin()
    {
        CreateNewRobot();
        InvokeRepeating("JudgeWhetherEnterNextEpisode", 5f, 5f); // judge next episode every 10 sec
    }

    // to specify agent behavior at every step, based on the provided actiona
    public override void OnActionReceived(ActionBuffers actions)
    {

        /* Left Leg, Foot*/
        Vector3 targetLeftLegAngularVelocity = new Vector3(
            actions.ContinuousActions[0],
            actions.ContinuousActions[1],
            actions.ContinuousActions[2]
            );
        Vector3 targetLeftFootAngularVelocity = new Vector3(
            actions.ContinuousActions[3],
            actions.ContinuousActions[4],
            actions.ContinuousActions[5]
            );

        /* Right leg, Foot */
        Vector3 targetRightLegAngularVelocity = new Vector3(
            actions.ContinuousActions[6],
            actions.ContinuousActions[7],
            actions.ContinuousActions[8]
            );
        Vector3 targetRightFootAngularVelocity = new Vector3(
            actions.ContinuousActions[9],
            actions.ContinuousActions[10],
            actions.ContinuousActions[11]
            );


        /* Left arm, hand */
        Vector3 targetLeftArmAngularVelocity = new Vector3(
            actions.ContinuousActions[12],
            actions.ContinuousActions[13],
            actions.ContinuousActions[14]
            );
        Vector3 targetLeftHandAngularVelocity = new Vector3(
            actions.ContinuousActions[15],
            actions.ContinuousActions[16],
            actions.ContinuousActions[17]
            );

        /* Right arm, hand */
        Vector3 targetRightArmAngularVelocity = new Vector3(
            actions.ContinuousActions[18],
            actions.ContinuousActions[19],
            actions.ContinuousActions[20]
            );
        Vector3 targetRightHandAngularVelocity = new Vector3(
            actions.ContinuousActions[21],
            actions.ContinuousActions[22],
            actions.ContinuousActions[23]
            );

        /* Chest and Head and Hips */
        Vector3 targetChestAngularVelocity = new Vector3(
            actions.ContinuousActions[24],
            actions.ContinuousActions[25],
            actions.ContinuousActions[26]
            );
        Vector3 targetHeadAngularVelocity = new Vector3(
            actions.ContinuousActions[27],
            actions.ContinuousActions[28],
            actions.ContinuousActions[29]
            );
        Vector3 targetHipsAngularVelocity = new Vector3(
            actions.ContinuousActions[24],
            actions.ContinuousActions[25],
            actions.ContinuousActions[26]
            );

        // output Left Leg, Foot
        robot.LeftLeg.AddAngularVelocity(10, 3, 10, targetLeftLegAngularVelocity);
        robot.LeftFoot.AddAngularVelocity(10, 3, 10, targetLeftFootAngularVelocity);

        // output right Leg, Foot
        robot.RightLeg.AddAngularVelocity(10, 3, 10, targetRightLegAngularVelocity);
        robot.RightFoot.AddAngularVelocity(10, 3, 10, targetRightFootAngularVelocity);

        // output Left shoulder, arm, hand
        robot.LeftArm.AddAngularVelocity(3, 2, 3, targetLeftArmAngularVelocity);
        robot.LeftHand.AddAngularVelocity(3, 2, 3, targetLeftHandAngularVelocity);

        // output Right shoulder, arm, hand
        robot.RightArm.AddAngularVelocity(3, 2, 3, targetRightArmAngularVelocity);
        robot.RightHand.AddAngularVelocity(3, 2, 3, targetRightHandAngularVelocity);

        robot.Hips.AddAngularVelocity(2, 2, 2, targetHipsAngularVelocity);

        robot.Chest.AddAngularVelocity(5, 5, 5, targetChestAngularVelocity);

        robot.Head.AddAngularVelocity(3, 3, 3, targetHeadAngularVelocity);
    }

    // to collect the vector observations of the agent for the step.(input)
    public override void CollectObservations(VectorSensor sensor)
    {
        /* Left Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftFoot.transform.position)); // position
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightLeg.transform.position)); // position
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.RightFoot.transform.position)); // position
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // -----------------------------------y

        /* Left Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftArm.transform.position)); // position
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftHand.transform.position)); // position
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.RightArm.transform.position)); // position
        sensor.AddObservation(robot.RightArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.RightHand.transform.position)); // position
        sensor.AddObservation(robot.RightHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity


        // -----------------------------------

        /* Hip */
        sensor.AddObservation(robot.Hips.transform.localPosition); // position
        sensor.AddObservation(robot.Hips.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Hips.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // -----------------------------------

        /* Chest */
        sensor.AddObservation(RelativeBodyPosition(robot.Chest.transform.position)); // position
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Head */
        sensor.AddObservation(RelativeBodyPosition(robot.Head.transform.position)); // position
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        //is touch floor
        sensor.AddObservation(robot.LeftLeg.isTouchFloor);
        sensor.AddObservation(robot.LeftFoot.isTouchFloor);
        sensor.AddObservation(robot.LeftArm.isTouchFloor);
        sensor.AddObservation(robot.LeftHand.isTouchFloor);
        sensor.AddObservation(robot.RightLeg.isTouchFloor);
        sensor.AddObservation(robot.RightArm.isTouchFloor);
        sensor.AddObservation(robot.RightHand.isTouchFloor);
        sensor.AddObservation(robot.Hips.isTouchFloor);
        sensor.AddObservation(robot.Chest.isTouchFloor);
        sensor.AddObservation(robot.Head.isTouchFloor);
        sensor.AddObservation((int)robot.footstate);
    }

    public void Heuristic(float[] actionsOut)
    {

    }

    private void CreateNewRobot()
    {
        if (robot != null)
        {
            Destroy(robot.gameObject);
        }
        robot = Instantiate(robotPrefab, new Vector3(transform.position.x + 10f, transform.position.y - 1f, transform.position.z), new Quaternion(0, 0, 0, 0)).GetComponent<CuteWalkman>();
        robot.transform.eulerAngles = new Vector3(0, 90f, 0);
        robot.transform.parent = transform;
        robot.RewardEvent.AddListener(AddReward);
    }

    private Vector3 RelativeBodyPosition(Vector3 position)
    {
        return position - robot.Hips.transform.position;
    }

    // Judge whether enter next episode

    private void JudgeWhetherEnterNextEpisode()
    {
        float actualPositionY = robot.Head.transform.localPosition.y;
        count++;
        if (actualPositionY < rewardUpperLimit)
        {
            count = 0;
            CancelInvoke("JudgeWhetherEnterNextEpisode");
            EndEpisode();
        }
        else
        {
            if (count == 10)
            {
                count = 0;
                CancelInvoke("JudgeWhetherEnterNextEpisode");
                EndEpisode();
            }
        }
    }
}
