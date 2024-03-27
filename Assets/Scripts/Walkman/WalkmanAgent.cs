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
    private int liveTime = 0;
    private float nowDistance = 0;

    // reset the agent or remove it from the environment
    public override void OnEpisodeBegin()
    {
        CreateNewRobot();
        nowDistance = RelativeFloorPosition(robot.LeftFoot.transform.position);
        InvokeRepeating("AddRewardToRobot", 0.5f, 0.5f); // add reward every 1 sec
        InvokeRepeating("JudgeWhetherEnterNextEpisode", 1f, 1f); // judge fall every 1 sec
        InvokeRepeating("JudgeWhetherEnterNextEpisode2", 30f, 30f); // judge next episode every 30 sec
        InvokeRepeating("DistanceReward", 5f, 5f); // judge distance every 5 sec
    }

    // to specify agent behavior at every step, based on the provided action
    public override void OnActionReceived(ActionBuffers actions)
    {

        /* Left Leg*/
        Vector3 targetLeftUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[0] * 20f,
            actions.ContinuousActions[1] * 0f,
            actions.ContinuousActions[2] * 0f
            );
        Vector3 targetLeftLegAngularVelocity = new Vector3(
            actions.ContinuousActions[3] * 20f,
            actions.ContinuousActions[4] * 5f,
            actions.ContinuousActions[5] * 20f
            );

        /* Right leg */
        Vector3 targetRightUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[6] * 20f,
            actions.ContinuousActions[7] * 0f,
            actions.ContinuousActions[8] * 0f
            );
        Vector3 targetRightLegAngularVelocity = new Vector3(
            actions.ContinuousActions[9] * 20f,
            actions.ContinuousActions[10] * 5f,
            actions.ContinuousActions[11] * 20f
            );


        /* Left shoulder, arm, hand */
        Vector3 targetLeftShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[12] * 10f,
            actions.ContinuousActions[13] * 5f,
            actions.ContinuousActions[14] * 10f
            );
        Vector3 targetLeftArmAngularVelocity = new Vector3(
            actions.ContinuousActions[15] * 10f,
            actions.ContinuousActions[16] * 5f,
            actions.ContinuousActions[17] * 10f
            );
        Vector3 targetLeftHandAngularVelocity = new Vector3(
            actions.ContinuousActions[18] * 10f,
            actions.ContinuousActions[19] * 5f,
            actions.ContinuousActions[20] * 10f
            );

        /* Right shoulder, arm, hand */
        Vector3 targetRightShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[21] * 10f,
            actions.ContinuousActions[22] * 5f,
            actions.ContinuousActions[23] * 10f
            );
        Vector3 targetRightArmAngularVelocity = new Vector3(
            actions.ContinuousActions[24] * 10f,
            actions.ContinuousActions[25] * 5f,
            actions.ContinuousActions[26] * 10f
            );
        Vector3 targetRightHandAngularVelocity = new Vector3(
            actions.ContinuousActions[27] * 10f,
            actions.ContinuousActions[28] * 5f,
            actions.ContinuousActions[29] * 10f
            );

        /* Chest and Waist and Head */
        Vector3 targetWaistAngularVelocity = new Vector3(
            actions.ContinuousActions[30] * 10f,
            actions.ContinuousActions[31] * 5f,
            actions.ContinuousActions[32] * 10f
            );

        Vector3 targetHeadAngularVelocity = new Vector3(
            actions.ContinuousActions[33] * 10f,
            actions.ContinuousActions[34] * 5f,
            actions.ContinuousActions[35] * 10f
            );

        /* Hips */
        Vector3 targetRightHipAngularVelocity = new Vector3(
            actions.ContinuousActions[36] * 20f,
            actions.ContinuousActions[37] * 5f,
            actions.ContinuousActions[38] * 20f
            );
        Vector3 targetLeftHipAngularVelocity = new Vector3(
            actions.ContinuousActions[39] * 20f,
            actions.ContinuousActions[40] * 5f,
            actions.ContinuousActions[41] * 20f
            );

        // output Left Leg, Foot
        robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftLegAngularVelocity;
        robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftUpLegAngularVelocity;

        // output right Leg, Foot
        robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightLegAngularVelocity;
        robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightUpLegAngularVelocity;

        // output Left shoulder, arm, hand
        robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftShoulderAngularVelocity;
        robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftArmAngularVelocity;
        robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetLeftHandAngularVelocity;

        // output Right shoulder, arm, hand
        robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightShoulderAngularVelocity;
        robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightArmAngularVelocity;
        robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetRightHandAngularVelocity;

        robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetWaistAngularVelocity;
        robot.Chest.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetWaistAngularVelocity;

        robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity = targetHeadAngularVelocity;
        robot.Hips.GetComponents<ConfigurableJoint>()[0].targetAngularVelocity = targetRightHipAngularVelocity;
        robot.Hips.GetComponents<ConfigurableJoint>()[1].targetAngularVelocity = targetLeftHipAngularVelocity;
    }

    // to collect the vector observations of the agent for the step.(input)
    public override void CollectObservations(VectorSensor sensor)
    {
        /* Left Up Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftUpLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftUpLeg.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Left Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftLeg.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Left Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftFoot.transform.position)); // position
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftFoot.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Up Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightUpLeg.transform.position)); // position
        sensor.AddObservation(robot.RightUpLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightUpLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightUpLeg.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightLeg.transform.position)); // position
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightLeg.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.RightFoot.transform.position)); // position
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightFoot.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        // -----------------------------------
        /* Left Shoulder */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftShoulder.transform.position)); // position
        sensor.AddObservation(robot.LeftShoulder.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftShoulder.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftShoulder.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Left Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftArm.transform.position)); // position
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftArm.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Left Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftHand.transform.position)); // position
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.LeftHand.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Shoulder */
        sensor.AddObservation(RelativeBodyPosition(robot.RightShoulder.transform.position)); // position
        sensor.AddObservation(robot.RightShoulder.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightShoulder.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightShoulder.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.RightArm.transform.position)); // position
        sensor.AddObservation(robot.RightArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightArm.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        /* Right Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.RightHand.transform.position)); // position
        sensor.AddObservation(robot.RightHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.RightHand.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity);


        // -----------------------------------

        /* Hip */
        sensor.AddObservation(robot.Hips.transform.localPosition); // position
        sensor.AddObservation(robot.Hips.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Hips.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Hips.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.Hips.GetComponent<ConfigurableJoint>().targetAngularVelocity);

        // -----------------------------------

        /* Chest */
        sensor.AddObservation(RelativeBodyPosition(robot.Chest.transform.position)); // position
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Chest.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.Chest.GetComponent<ConfigurableJoint>().targetAngularVelocity);
        /* Waist */
        sensor.AddObservation(RelativeBodyPosition(robot.Waist.transform.position)); // position
        sensor.AddObservation(robot.Waist.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Waist.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Waist.transform.rotation.eulerAngles); // angle
        sensor.AddObservation(robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity);
        /* Head */
        sensor.AddObservation(RelativeBodyPosition(robot.Head.transform.position)); // position 
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Head.transform.rotation.eulerAngles); // angle
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
        //AddReward((robot.Hips.transform.localPosition.y > 2.15) ? 1 : -1);
        //AddReward((robot.Chest.transform.localPosition.y > 2.45) ? 1 : -1);
        AddReward(robot.Hips.transform.localPosition.y -1f);
        AddReward(robot.Chest.transform.localPosition.y - 1f);
        //float eulerHipZ = robot.Hips.transform.eulerAngles.z < 180 ? robot.Hips.transform.eulerAngles.z : 360 - robot.Hips.transform.eulerAngles.z;
        //float eulerHipX = robot.Hips.transform.eulerAngles.x < 180 ? robot.Hips.transform.eulerAngles.x : 360 - robot.Hips.transform.eulerAngles.x;
        //AddReward((45 - eulerHipZ - eulerHipX) / 45);

        float eulerChestY = robot.Chest.transform.localEulerAngles.y < 180 ? robot.Chest.transform.localEulerAngles.y : 360 - robot.Chest.transform.localEulerAngles.y;
        AddReward((10 - eulerChestY) / 10);

        AddReward(1f - math.abs(0.2f - (robot.RightFoot.transform.position.z - robot.LeftFoot.transform.position.z)) * 5);
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

    private float RelativeFloorPosition(Vector3 position)
    {
        return position.x - transform.position.x;
    }

    // Judge whether enter next episode
    
    private void JudgeWhetherEnterNextEpisode()
    {
        float actualPositionY = robot.Head.transform.localPosition.y;
        liveTime += 1;
        if (actualPositionY < rewardUpperLimit)
        {
            AddReward(liveTime);
            liveTime = 0;
            CancelInvoke("AddRewardToRobot");
            CancelInvoke("JudgeWhetherEnterNextEpisode");
            CancelInvoke("JudgeWhetherEnterNextEpisode2");
            EndEpisode();
        }
    }

    private void JudgeWhetherEnterNextEpisode2()
    {
        CancelInvoke("AddRewardToRobot");
        CancelInvoke("JudgeWhetherEnterNextEpisode");
        CancelInvoke("JudgeWhetherEnterNextEpisode2");
        EndEpisode();
    }

    private void DistanceReward()
    {
        float rightFootX = RelativeFloorPosition(robot.RightFoot.transform.position);
        float leftFootX = RelativeFloorPosition(robot.LeftFoot.transform.position);
        if (rightFootX < leftFootX)
        {
            AddReward((nowDistance - leftFootX)*3);
            nowDistance = leftFootX;
        }
        else
        {
            AddReward((nowDistance - rightFootX) * 3);
            nowDistance = rightFootX;
        }
            
    }
}
