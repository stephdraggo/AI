﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Filter/Other Flock")]
    public class OtherFlock : ContextFilter
    {
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            List<Transform> filtered = new List<Transform>(); //new filtered list
            foreach (Transform item in original) //for each neighbour in the original list
            {
                FlockAgent itemAgent = item.GetComponent<FlockAgent>(); //get the flock agent of the neighbour
                if (itemAgent != null && itemAgent.AgentFlock != agent.AgentFlock) //if it does have a flock agent and it's a different flock from the agent
                {
                    filtered.Add(item); //add neighbour to filtered list
                }
            }
            return filtered;
        }
    }
}