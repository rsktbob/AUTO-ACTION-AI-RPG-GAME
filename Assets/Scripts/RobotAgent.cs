using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Unity.MLAgents.Actuators;
using System;

public class RobotAgent : Agent
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private float InitPositionMinY;
    [SerializeField] private float InitPositionMaxY;
    [SerializeField] private float rewardUpperLimit;
    private Robot robot;

    public override void OnEpisodeBegin()
    {
        InvokeRepeating("AddRewardToRobot", 1f, 1f);
        InvokeRepeating("JudgeWhetherEnterNextEpisode", 10f, 10f);
        CreateNewRobot();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Left Leg
        Vector3 targetLeftLegAngularVelocity = new Vector3(
            actions.ContinuousActions[0],
            actions.ContinuousActions[1],
            actions.ContinuousActions[2]
            ) * 5;

        // Left Foot
        Vector3 targetLeftFootAngularVelocity = new Vector3(
            actions.ContinuousActions[3],
            actions.ContinuousActions[4],
            actions.ContinuousActions[5]
            ) * 5;

        // Right Leg
        Vector3 targetRightLegAngularVelocity = new Vector3(
            actions.ContinuousActions[6],
            actions.ContinuousActions[7],
            actions.ContinuousActions[8]
            ) * 5;

        // Right Foot
        Vector3 targetRightFootAngularVelocity = new Vector3(
            actions.ContinuousActions[9],
            actions.ContinuousActions[10],
            actions.ContinuousActions[11]
            ) * 5;

        // Body
        Vector3 targetBodyAngularVelocity = new Vector3(
            actions.ContinuousActions[12],
            actions.ContinuousActions[13],
            actions.ContinuousActions[14]
            ) * 3;

        // Head
        Vector3 targetHeadAngularVelocity = new Vector3(
            actions.ContinuousActions[12],
            actions.ContinuousActions[13],
            actions.ContinuousActions[14]
            ) * 5;

        // Input Left Leg
        robot.leftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftLegAngularVelocity;

        // Input Left Foot
        robot.leftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftFootAngularVelocity;

        // Input Right Leg
        robot.rightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightLegAngularVelocity;

        // Input Right Foot
        robot.rightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightFootAngularVelocity;

        // Input Head
        robot.head.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetHeadAngularVelocity;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Left Leg
        sensor.AddObservation(RelativeBodyPosition(robot.leftFoot.transform.position)); // position
        sensor.AddObservation(robot.leftLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.leftLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // Left Foot
        sensor.AddObservation(RelativeBodyPosition(robot.leftFoot.transform.position)); // position
        sensor.AddObservation(robot.leftFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.leftFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // Right Leg
        sensor.AddObservation(RelativeBodyPosition(robot.rightLeg.transform.position)); // position
        sensor.AddObservation(robot.rightLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.rightLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // Right Foot
        sensor.AddObservation(RelativeBodyPosition(robot.rightFoot.transform.position)); // position
        sensor.AddObservation(robot.rightFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.rightFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // Body
        sensor.AddObservation(RelativeBodyPosition(robot.body.transform.position)); // position
        sensor.AddObservation(robot.body.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.body.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // Head
        sensor.AddObservation(RelativeBodyPosition(robot.head.transform.position)); // position
        sensor.AddObservation(robot.head.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.head.GetComponent<Rigidbody>().angularVelocity); // angular velocity
    }

    public void Heuristic(float[] actionsOut)
    {

    }

    private Vector3  RelativeBodyPosition(Vector3 position)
    {
        return position - robot.body.transform.position;
    }

    //Add reward to robot
    private void AddRewardToRobot()
    {   
        float leftFootToHeadDistance = robot.head.transform.localPosition.y - robot.leftFoot.transform.localPosition.y;
        float rightFootToHeadDistance = robot.head.transform.localPosition.y - robot.rightFoot.transform.localPosition.y;

        // Left Foot to head height distance reward
        if (leftFootToHeadDistance > 1.3)
        {
            AddReward(0.1f);
        }
        else if (leftFootToHeadDistance >= 1 && leftFootToHeadDistance <= 1.3)
        {
            AddReward(0.05f);
        }
        else
        {
            AddReward(-1f);
        }

        // Right Foot to head height distance reward
        if (rightFootToHeadDistance > 1.3)
        {
            AddReward(0.1f);
        }
        else if (rightFootToHeadDistance >= 1 && rightFootToHeadDistance <= 1.3)
        {
            AddReward(0.05f);
        }
        else
        {
            AddReward(-1f);
        }

        // Height reward
        float heightReward = robot.head.transform.localPosition.y - rewardUpperLimit;
        heightReward = heightReward > 0 ? (float)Math.Pow(20, heightReward) + heightReward : heightReward;
        AddReward(heightReward);
    }

    // Create new robot
    private void CreateNewRobot()
    {
        if (robot != null)
        {
            Destroy(robot.gameObject);
        }
        robot = Instantiate(robotPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)).GetComponent<Robot>();
        robot.transform.parent = transform;
        robot.transform.localPosition = new Vector3(0, 0, 0);
        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(-2, 2),
            UnityEngine.Random.Range(InitPositionMinY, InitPositionMaxY),
            UnityEngine.Random.Range(-2, 2));
        Console.WriteLine(randomPosition);
        robot.Move(randomPosition);
    }

    // Judge whether enter next episode
    private void JudgeWhetherEnterNextEpisode()
    {
        float actualPositionY = robot.head.transform.localPosition.y;
        if (actualPositionY < rewardUpperLimit)
        {
            CancelInvoke("AddRewardToRobot");
            CancelInvoke("JudgeWhetherEnterNextEpisode");
            EndEpisode();
        }
    }
}