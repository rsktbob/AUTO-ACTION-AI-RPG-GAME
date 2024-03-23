using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;
using Unity.Mathematics;
using UnityEditor;

public class WalkmanAgent : Agent
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private float rewardUpperLimit;
    private Walkman robot;

    // reset the agent or remove it from the environment
    public override void OnEpisodeBegin()
    {
        CreateNewRobot();
        InvokeRepeating("AddRewardToRobot", 0.1f, 0.1f); // add reward every 1 sec
        InvokeRepeating("JudgeWhetherEnterNextEpisode", 5f, 5f); // judge next episode every 10 sec
        InvokeRepeating("JudgeWhetherEnterNextEpisode2", 20f, 20f); // judge next episode every 10 sec
    }

    // to specify agent behavior at every step, based on the provided action
    public override void OnActionReceived(ActionBuffers actions)
    {

        /* Left Leg, Foot*/
        Vector3 targetLeftUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[0],
            actions.ContinuousActions[1],
            actions.ContinuousActions[2]
            ) * 2;
        Vector3 targetLeftLegAngularVelocity = new Vector3(
            actions.ContinuousActions[3],
            actions.ContinuousActions[4],
            actions.ContinuousActions[5]
            ) * 2;
        Vector3 targetLeftFootAngularVelocity = new Vector3(
            actions.ContinuousActions[6],
            actions.ContinuousActions[7],
            actions.ContinuousActions[8]
            ) * 2;

        /* Right leg, Foot */
        Vector3 targetRightUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[9],
            actions.ContinuousActions[10],
            actions.ContinuousActions[11]
            )*2;
        Vector3 targetRightLegAngularVelocity = new Vector3(
            actions.ContinuousActions[12],
            actions.ContinuousActions[13],
            actions.ContinuousActions[14]
            ) * 2;
        Vector3 targetRightFootAngularVelocity = new Vector3(
            actions.ContinuousActions[15],
            actions.ContinuousActions[16],
            actions.ContinuousActions[17]
            ) * 2;


        /* Left shoulder, arm, hand */
        Vector3 targetLeftShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[18],
            actions.ContinuousActions[19],
            actions.ContinuousActions[20]
            );
        Vector3 targetLeftArmAngularVelocity = new Vector3(
            actions.ContinuousActions[21],
            actions.ContinuousActions[22],
            actions.ContinuousActions[23]
            );
        Vector3 targetLeftHandAngularVelocity = new Vector3(
            actions.ContinuousActions[24],
            actions.ContinuousActions[25],
            actions.ContinuousActions[26]
            );

        /* Right shoulder, arm, hand */
        Vector3 targetRightShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[27],
            actions.ContinuousActions[28],
            actions.ContinuousActions[29]
            );
        Vector3 targetRightArmAngularVelocity = new Vector3(
            actions.ContinuousActions[30],
            actions.ContinuousActions[31],
            actions.ContinuousActions[32]
            );
        Vector3 targetRightHandAngularVelocity = new Vector3(
            actions.ContinuousActions[33],
            actions.ContinuousActions[34],
            actions.ContinuousActions[35]
            );
        /* Hips */
        Vector3 targetHipAngularVelocity = new Vector3(
            actions.ContinuousActions[36],
            actions.ContinuousActions[37],
            actions.ContinuousActions[38]
            );

        /* Chest and Waist and Head */
        Vector3 targetWaistAngularVelocity = new Vector3(
            actions.ContinuousActions[39],
            actions.ContinuousActions[40],
            actions.ContinuousActions[41]
            );

        Vector3 targetHeadAngularVelocity = new Vector3(
            actions.ContinuousActions[42],
            actions.ContinuousActions[43],
            actions.ContinuousActions[44]
            );

        // output Left Leg, Foot
        robot.LeftLeg.AddAngularVelocity(20, 0, 0, targetLeftLegAngularVelocity);
        robot.LeftUpLeg.AddAngularVelocity(30, 10, 30, targetLeftUpLegAngularVelocity);
        robot.LeftFoot.AddAngularVelocity(20, 7, 20, targetLeftFootAngularVelocity);

        // output right Leg, Foot
        robot.RightLeg.AddAngularVelocity(20, 0, 0, targetRightLegAngularVelocity);
        robot.RightUpLeg.AddAngularVelocity(30, 10, 30, targetRightUpLegAngularVelocity);
        robot.RightFoot.AddAngularVelocity(20, 7, 20, targetRightFootAngularVelocity);

        // output Left shoulder, arm, hand
        robot.LeftShoulder.AddAngularVelocity(5, 2, 5, targetLeftShoulderAngularVelocity);
        robot.LeftArm.AddAngularVelocity(5, 2, 5, targetLeftArmAngularVelocity);
        robot.LeftHand.AddAngularVelocity(5, 2, 5, targetLeftHandAngularVelocity);

        // output Right shoulder, arm, hand
        robot.RightShoulder.AddAngularVelocity(5, 2, 5, targetRightShoulderAngularVelocity);
        robot.RightArm.AddAngularVelocity(5, 2, 5, targetRightArmAngularVelocity);
        robot.RightHand.AddAngularVelocity(5, 2, 5, targetRightHandAngularVelocity);

        robot.Hips.AddAngularVelocity(30, 5, 13, targetHipAngularVelocity);

        robot.Waist.AddAngularVelocity(20, 5, 20, targetWaistAngularVelocity);
        robot.Chest.AddAngularVelocity(20, 5, 20, targetWaistAngularVelocity);

        robot.Head.AddAngularVelocity(10, 3, 10, targetHeadAngularVelocity);
    }

    // to collect the vector observations of the agent for the step.(input)
    public override void CollectObservations(VectorSensor sensor)
    {
        /* Left Up Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftUpLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Left Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Left Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftFoot.transform.position)); // position
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Up Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightUpLeg.transform.position)); // position
        sensor.AddObservation(robot.RightUpLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightUpLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightLeg.transform.position)); // position
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.RightFoot.transform.position)); // position
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        // -----------------------------------
        /* Left Shoulder */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftShoulder.transform.position)); // position
        sensor.AddObservation(robot.LeftShoulder.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftShoulder.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Left Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftArm.transform.position)); // position
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Left Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftHand.transform.position)); // position
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Shoulder */
        sensor.AddObservation(RelativeBodyPosition(robot.RightShoulder.transform.position)); // position
        sensor.AddObservation(robot.RightShoulder.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightShoulder.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.RightArm.transform.position)); // position
        sensor.AddObservation(robot.RightArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.RightHand.transform.position)); // position
        sensor.AddObservation(robot.RightHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity);


        // -----------------------------------

        /* Hip */
        sensor.AddObservation(robot.Hips.transform.localPosition); // position
        sensor.AddObservation(robot.Hips.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Hips.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Hips.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        // -----------------------------------

        /* Chest */
        sensor.AddObservation(RelativeBodyPosition(robot.Chest.transform.position)); // position
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Chest.GetComponent<ConfigurableJoint>().targetAngularVelocity);
        /* Waist */
        sensor.AddObservation(RelativeBodyPosition(robot.Waist.transform.position)); // position
        sensor.AddObservation(robot.Waist.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Waist.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity);
        /* Head */
        sensor.AddObservation(RelativeBodyPosition(robot.Head.transform.position)); // position
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        //is touch floor
        sensor.AddObservation(robot.LeftUpLeg.isTouchFloor);
        sensor.AddObservation(robot.LeftLeg.isTouchFloor);
        sensor.AddObservation(robot.LeftFoot.isTouchFloor);
        sensor.AddObservation(robot.RightUpLeg.isTouchFloor);
        sensor.AddObservation(robot.RightLeg.isTouchFloor);
        sensor.AddObservation(robot.RightFoot.isTouchFloor);
        sensor.AddObservation(robot.LeftShoulder.isTouchFloor);
        sensor.AddObservation(robot.RightArm.isTouchFloor);
        sensor.AddObservation(robot.RightHand.isTouchFloor);
        sensor.AddObservation(robot.Hips.isTouchFloor);
        sensor.AddObservation(robot.Chest.isTouchFloor);
        sensor.AddObservation(robot.Waist.isTouchFloor);
        sensor.AddObservation(robot.Head.isTouchFloor);
        sensor.AddObservation((int)robot.footstate);
    }

    //Add reward to robot
    private void AddRewardToRobot() 
    {
        AddReward(robot.Hips.transform.localPosition.y > 2.1f ? 1 : -1);
        AddReward(robot.Chest.transform.localPosition.y > 2.4f ? 1 : -1);
        //AddReward(robot.Hips.transform.localPosition.y - 0.5f);
        //AddReward(robot.Chest.transform.localPosition.y - 0.5f);
        //float eulerLeftUpLegJointZ = Mathf.Abs(Mathf.DeltaAngle(robot.LeftUpLeg.transform.eulerAngles.z, robot.Hips.transform.eulerAngles.z));
        //float eulerRightUpLegJointZ = Mathf.Abs(Mathf.DeltaAngle(robot.RightUpLeg.transform.eulerAngles.z, robot.Hips.transform.eulerAngles.z));
        //float eulerLeftUpLegJointY = Mathf.Abs(Mathf.DeltaAngle(robot.LeftUpLeg.transform.eulerAngles.y, robot.Hips.transform.eulerAngles.y));
        //float eulerRightUpLegJointY = Mathf.Abs(Mathf.DeltaAngle(robot.RightUpLeg.transform.eulerAngles.y, robot.Hips.transform.eulerAngles.y));

        float eulerLeftFootX = robot.LeftFoot.transform.eulerAngles.x < 180 ? robot.LeftFoot.transform.eulerAngles.x : 360 - robot.LeftFoot.transform.eulerAngles.x;
        float eulerLeftFootZ = robot.LeftFoot.transform.eulerAngles.z < 180 ? robot.LeftFoot.transform.eulerAngles.z : 360 - robot.LeftFoot.transform.eulerAngles.z;
        float eulerRightFootX = robot.RightFoot.transform.eulerAngles.x < 180 ? robot.RightFoot.transform.eulerAngles.x : 360 - robot.RightFoot.transform.eulerAngles.x;
        float eulerRightFootZ = robot.RightFoot.transform.eulerAngles.z < 180 ? robot.RightFoot.transform.eulerAngles.z : 360 - robot.RightFoot.transform.eulerAngles.z;
        float eulerHipZ = robot.Hips.transform.eulerAngles.z < 180 ? robot.Hips.transform.eulerAngles.z : 360 - robot.Hips.transform.eulerAngles.z;

        //float eulerChestX = robot.Chest.transform.eulerAngles.x < 180 ? robot.Chest.transform.eulerAngles.x : 360 - robot.Chest.transform.eulerAngles.x;
        //float eulerChestZ = robot.Chest.transform.eulerAngles.z < 180 ? robot.Chest.transform.eulerAngles.z : 360 - robot.Chest.transform.eulerAngles.z;
        AddReward((45 - eulerLeftFootX - eulerLeftFootZ) / 100);
        AddReward((45 - eulerRightFootX - eulerRightFootZ) / 100);
        AddReward((20 - eulerHipZ) / 20);
        float eulerChestY = robot.Chest.transform.localEulerAngles.y < 180 ? robot.Chest.transform.localEulerAngles.y : 360 - robot.Chest.transform.localEulerAngles.y;
        AddReward((10 - eulerChestY) / 10);
        //AddReward((10 - eulerLeftUpLegJointZ) / 20);
        //AddReward((10 - eulerRightUpLegJointZ) / 20);
        //AddReward((20 - eulerLeftUpLegJointY) / 80);
        //AddReward((20 - eulerRightUpLegJointY) / 80);

        //AddReward((30 - eulerChestX) / 20);
        //AddReward((30 - eulerChestZ) / 20);
        AddReward(robot.LeftFoot.transform.position.z+0.15 > robot.RightFoot.transform.position.z ? -10 : 0);
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
        robot = Instantiate(robotPrefab, new Vector3(transform.position.x + 10f, transform.position.y - 1f, transform.position.z), new Quaternion(0,0,0,0)).GetComponent<Walkman>();
        robot.transform.eulerAngles = new Vector3(0, 90f, 0);
        robot.transform.parent = transform;
        robot.RewardEvent.AddListener(AddReward);
    }

    private Vector3 RelativeBodyPosition(Vector3 position)
    {
        return position - robot.Hips.transform.position;
    }

    private Vector3 RelativeFloorPosition(Vector3 position)
    {
        return position - transform.position;
    }

    // Judge whether enter next episode
    
    private void JudgeWhetherEnterNextEpisode()
    {
        float actualPositionY = robot.Head.transform.localPosition.y;
        if (actualPositionY < rewardUpperLimit)
        {
            CancelInvoke("AddRewardToRobot");
            CancelInvoke("JudgeWhetherEnterNextEpisode");
            CancelInvoke("JudgeWhetherEnterNextEpisode2");
            EndEpisode();
        }
    }

    private void JudgeWhetherEnterNextEpisode2()
    {
        float actualPositionX = RelativeFloorPosition(robot.Hips.transform.position).x;
        if (actualPositionX > -9)
        {
            CancelInvoke("AddRewardToRobot");
            CancelInvoke("JudgeWhetherEnterNextEpisode");
            CancelInvoke("JudgeWhetherEnterNextEpisode2");
            EndEpisode();
        }
    }
}
