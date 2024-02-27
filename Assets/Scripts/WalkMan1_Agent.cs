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
    private Walkman walkman;

    // reset the agent or remove it from the environment
    public override void OnEpisodeBegin()
    {
    }

    // to specify agent behavior at every step, based on the provided action
    public override void OnActionReceived(ActionBuffers actions)
    {
    }

    // to collect the vector observations of the agent for the step.
    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public void Heuristic(float[] actionsOut)
    {

    }

    /*
    private Vector3 RelativeBodyPosition(Vector3 position)
    {

    }
    */

    private void AddRewardToRobot() { 
    }
}
