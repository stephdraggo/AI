﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [AddComponentMenu("AI/Flock Behaviour Base")]
    //ScriptableObject means it can run without a game object
    //abstract forces children to implement method
    public abstract class FlockBehaviour : ScriptableObject
    {

        /// <summary>Calculate direction to move agent according to its position, its neighbours and its flock.</summary>
        /// <param name="agent">The agent being moved.</param>
        /// <param name="context">The list of neighbours.</param>
        /// <param name="flock">The flock of the agent.</param>
        /// <returns>Vector2</returns>
        public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
    }
}