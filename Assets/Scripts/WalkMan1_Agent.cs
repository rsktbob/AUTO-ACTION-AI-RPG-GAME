using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;


public class WalkMan1_Agent : Agent
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private float InitPositionMinY;
    [SerializeField] private float InitPositionMaxY;
    [SerializeField] private float rewardUpperLimit;
    private Walkman robot;

    // reset the agent or remove it from the environment
    public override void OnEpisodeBegin()
    {
        InvokeRepeating("AddRewardToRobot", 1f, 1f); // add reward every 1 sec
        InvokeRepeating("JudgeWhetherEnterNextEpisode", 10f, 10f); // judge next episode every 10 sec
        CreateNewRobot();
    }

    // to specify agent behavior at every step, based on the provided action
    public override void OnActionReceived(ActionBuffers actions)
    {
        /* Left Leg, Foot, Toe */
        Vector3 targetLeftUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[0],
            actions.ContinuousActions[1],
            actions.ContinuousActions[2]
            ) * 100;
        Vector3 targetLeftLegAngularVelocity = new Vector3(
            actions.ContinuousActions[3],
            actions.ContinuousActions[4],
            actions.ContinuousActions[5]
            ) * 100;
        Vector3 targetLeftFootAngularVelocity = new Vector3(
            actions.ContinuousActions[6],
            actions.ContinuousActions[7],
            actions.ContinuousActions[8]
            ) * 100;
        Vector3 targetLeftToeAngularVelocity = new Vector3(
            actions.ContinuousActions[9],
            actions.ContinuousActions[10],
            actions.ContinuousActions[11]
            ) * 100;

        /* Right leg, Foot, Toe */
        Vector3 targetRightUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[12],
            actions.ContinuousActions[13],
            actions.ContinuousActions[14]
            ) * 100;
        Vector3 targetRightLegAngularVelocity = new Vector3(
            actions.ContinuousActions[15],
            actions.ContinuousActions[16],
            actions.ContinuousActions[17]
            ) * 100;
        Vector3 targetRightFootAngularVelocity = new Vector3(
            actions.ContinuousActions[18],
            actions.ContinuousActions[19],
            actions.ContinuousActions[20]
            ) * 100;
        Vector3 targetRightToeAngularVelocity = new Vector3(
            actions.ContinuousActions[21],
            actions.ContinuousActions[22],
            actions.ContinuousActions[23]
            ) * 100;

        /* Left shoulder, arm, hand */
        Vector3 targetLeftShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[24],
            actions.ContinuousActions[25],
            actions.ContinuousActions[26]
            ) * 20;
        Vector3 targetLeftArmAngularVelocity = new Vector3(
            actions.ContinuousActions[27],
            actions.ContinuousActions[28],
            actions.ContinuousActions[29]
            ) * 17;
        Vector3 targetLeftHandAngularVelocity = new Vector3(
            actions.ContinuousActions[30],
            actions.ContinuousActions[31],
            actions.ContinuousActions[32]
            ) * 17;

        /* Right shoulder, arm, hand */
        Vector3 targetRightShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[33],
            actions.ContinuousActions[34],
            actions.ContinuousActions[35]
            ) * 20;
        Vector3 targetRightArmAngularVelocity = new Vector3(
            actions.ContinuousActions[36],
            actions.ContinuousActions[37],
            actions.ContinuousActions[38]
            ) * 17;
        Vector3 targetRightHandAngularVelocity = new Vector3(
            actions.ContinuousActions[39],
            actions.ContinuousActions[40],
            actions.ContinuousActions[41]
            ) * 17;

        /* Hip */
        Vector3 targetHipAngularVelocity = new Vector3(
            actions.ContinuousActions[42],
            actions.ContinuousActions[43],
            actions.ContinuousActions[44]
            ) * 160;

        /* Chest and Waist and Head */
        Vector3 targetWaistAngularVelocity = new Vector3(
            actions.ContinuousActions[45],
            actions.ContinuousActions[46],
            actions.ContinuousActions[47]
            ) * 45;
        Vector3 targetChestAngularVelocity = new Vector3(
            actions.ContinuousActions[48],
            actions.ContinuousActions[49],
            actions.ContinuousActions[50]
            ) * 25;

        Vector3 targetHeadAngularVelocity = new Vector3(
            actions.ContinuousActions[51],
            actions.ContinuousActions[52],
            actions.ContinuousActions[53]
            ) * 7;


        // output Left Leg, Foot, Toe
        robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftLegAngularVelocity;
        robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftUpLegAngularVelocity;
        robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftFootAngularVelocity;
        robot.LeftToe.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftToeAngularVelocity;

        // output right Leg, Foot, Toe
        robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightLegAngularVelocity;
        robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightUpLegAngularVelocity;
        robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightFootAngularVelocity;
        robot.RightToe.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightToeAngularVelocity;

        // output Left shoulder, arm, hand
        robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftShoulderAngularVelocity;
        robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftArmAngularVelocity;
        robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftHandAngularVelocity;

        // output Right shoulder, arm, hand
        robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightShoulderAngularVelocity;
        robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightArmAngularVelocity;
        robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightHandAngularVelocity;

        // output Hip
        robot.Hip.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetHipAngularVelocity;

        // output Chest, Waist, Head
        robot.Chest.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetChestAngularVelocity;
        robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetWaistAngularVelocity;
        robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetHeadAngularVelocity;


    }

    // to collect the vector observations of the agent for the step.(input)
    public override void CollectObservations(VectorSensor sensor)
    {
        /* Left Up Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftUpLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftFoot.transform.position)); // position
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Toe */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftToe.transform.position)); // position
        sensor.AddObservation(robot.LeftToe.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftToe.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Up Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightUpLeg.transform.position)); // position
        sensor.AddObservation(robot.RightUpLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightUpLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightLeg.transform.position)); // position
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.RightFoot.transform.position)); // position
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Toe */
        sensor.AddObservation(RelativeBodyPosition(robot.RightToe.transform.position)); // position
        sensor.AddObservation(robot.RightToe.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightToe.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // -----------------------------------
        /* Left Shoulder */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftShoulder.transform.position)); // position
        sensor.AddObservation(robot.LeftShoulder.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftShoulder.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftArm.transform.position)); // position
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftHand.transform.position)); // position
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Shoulder */
        sensor.AddObservation(RelativeBodyPosition(robot.RightShoulder.transform.position)); // position
        sensor.AddObservation(robot.RightShoulder.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightShoulder.GetComponent<Rigidbody>().angularVelocity); // angular velocity

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
        sensor.AddObservation(RelativeBodyPosition(robot.Hip.transform.position)); // position
        sensor.AddObservation(robot.Hip.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Hip.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        
        // -----------------------------------
        
        /* Chest */
        sensor.AddObservation(robot.Chest.transform.localPosition); // position
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        /* Waist */
        sensor.AddObservation(RelativeBodyPosition(robot.Waist.transform.position)); // position
        sensor.AddObservation(robot.Waist.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Waist.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        /* Head */
        sensor.AddObservation(RelativeBodyPosition(robot.Head.transform.position)); // position
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().angularVelocity); // angular velocity
    }

    //Add reward to robot
    private void AddRewardToRobot() 
    {
        float LeftFootToHeadDistance = robot.Head.transform.localPosition.y - robot.LeftFoot.transform.localPosition.y;
        float RightFootToHeadDistance = robot.Head.transform.localPosition.y - robot.RightFoot.transform.localPosition.y;

        // Left Foot to head height distance reward
        if (LeftFootToHeadDistance > 4.2)
        {
            AddReward(0.1f);
        }
        else if (LeftFootToHeadDistance >= 4 && LeftFootToHeadDistance <= 4.2)
        {
            AddReward(0.05f);
        }
        else
        {
            AddReward(-1f);
        }
        // Right Foot to head height distance reward
        if (RightFootToHeadDistance > 4.2)
        {
            AddReward(0.1f);
        }
        else if (RightFootToHeadDistance >= 4 && RightFootToHeadDistance <= 4.2)
        {
            AddReward(0.05f);
        }
        else
        {
            AddReward(-1f);
        }

        // Height reward
        float heightReward = robot.Head.transform.localPosition.y - rewardUpperLimit;
        heightReward = heightReward > 0 ? (float)Math.Pow(20, heightReward) + heightReward : heightReward;
        AddReward(heightReward);
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
        robot = Instantiate(robotPrefab, new Vector3(transform.position.x, transform.position.y+ 0.5f, transform.position.z), new Quaternion(0,0,0,0)).GetComponent<Walkman>();
        robot.transform.parent = transform;
        
    }

    private Vector3 RelativeBodyPosition(Vector3 position)
    {
        return position - robot.Chest.transform.position;
    }

    // Judge whether enter next episode
    private void JudgeWhetherEnterNextEpisode()
    {
        float actualPositionY = robot.Head.transform.localPosition.y;
        if (actualPositionY < rewardUpperLimit)
        {
            CancelInvoke("AddRewardToRobot");
            CancelInvoke("JudgeWhetherEnterNextEpisode");
            EndEpisode();
        }
    }
}
